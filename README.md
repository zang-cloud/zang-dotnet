zang-dotnet
===========

This .net library is an open source tool built to simplify interaction with the [Zang](http://www.zang.io) telephony platform. Zang makes adding voice and SMS to applications fun and easy.

For more information about Zang, please visit: [Zang Cloud](https://www.zang.io/products/cloud)

To read the official documentation, please visit: [Zang Docs](http://docs.zang.io/aspx/docs)

---


Installation
============

Windows
##### 1. Using DLL

Download the zip of this repo and add the desired DLLs to your project.

##### 1. Using Visual Studio and NuGet Package Manager

- In Solution Explorer, right click on References and choose Manage NuGet Packages.
- Choose nuget.org as the Package source, click the Browse tab, search for ZangAPI, select that package in the list, and click Install
- Right-click the solution in Solution Explorer and click Build Solution

Usage
======

### REST

See the [Zang REST API documentation](http://docs.zang.io/aspx/rest) for more information.

#### Make call Example

```cs
using System;
using ZangAPI.Configuration;
using ZangAPI.Exceptions;

public class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ZangConfiguration({AccountSid}, {AuthToken});
			var service = new ZangService(configuration);

		public void MakeCall()
        {
            try
            {
                // Make call using calls connector
                var call = service.CallsConnector.MakeCall("+12345", "+12678", "http://zang.io/ivr/welcome/call", playDtmf: "ww12w3221", timeout: 100);
                Console.WriteLine(call.Status);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```


#### Configuration (3 ways)

First, a configuration must be created by using provided configuration class `ZangConfiguration` or by creating your own implementation of `IZangConfiguration` interface.

##### 1. Create ZangConfiguration with parameters AccountSid and AuthToken

Normally you'll want to just enter your Zang Platform *AccountSid* and *AuthToken*, but you can also define a proxy server or change the base API URL.
Basic configuration:

```cs
var configuration = new ZangConfiguration({AccountSid}, {AuthToken});
```
or if you want to define a proxy server:
```cs
var configuration = new ZangConfiguration({AccountSid}, {AuthToken});
configuration.UseProxy = true;
configuration.ProxyHost = {ProxyHost}
configuration.ProxyPort = {ProxyPort}
```
Next, you'll have to create a ZangService and pass the configuration
```cs
var service = new ZangService(configuration)
```

##### 2. Use a custom HttpManager
Another way to create ZangService is to pass a custom HttpManager which implements IHttpManager. 
```cs
var service = new ZangService(myHttpManager)
```

##### 3. Use configuration parameters from `app.config` file

Create ZangService with empty ZangConfiguration
```cs
var service = new ZangService(new ZangConfiguration("", ""));
```

Configure service 
```cs
service.InitFromConfig();
```


#### Request parameters
Request parameters are passed as parameters to connector object methods as shown previously in *Send SMS Example*. All methods have convenience overloads which use the `AccountSid` parameter specified in the configuration automatically.

Explicit accountSid
```cs
var usage = service.UsagesConnector.ViewUsage({accountSid}, {UsageSid});
```

AccountSid from configuration used automatically
```cs
var usage = service.UsagesConnector.ViewUsage({UsageSid});
```

##### Methods with optional parameters

Optional parameters are used as named parameters like `transcribe:true`, e.g:
```cs
var call = service.CallsConnector.MakeCall("+12345", "+112233", "testUrl", transcribe:true, transcribeCallback:"transcribeCallback");
```

#### Response data
The received data can be an object, e.g.:

```cs
var usage = service.UsagesConnector.ViewUsage({UsageSid});
Console.WriteLine(usage.TotalCost);
```
Or a list of objects in which case the list is iterable, e.g.:
```cs
var usages = service.UsagesConnector.ListUsages(year:2017, month:5, product:Product.INBOUND_CALL, page: 3, pageSize: 40);

foreach (var usage in usages.Elements)
{
      Console.WriteLine(usage.TotalCost);              
}
```

### InboundXML

InboundXML is an XML dialect which enables you to control phone call flow. For more information please visit the [Zang InboundXML documentation](http://docs.zang.io/aspx/inboundxml).

Example of using InboundXML builder:

```cs
using System;
using ZangAPI.InboundXml;
using ZangAPI.Exceptions;

public class Program
{
    static void Main(string[] args)
    {
    	try
        {
            var builder = new InboundXmlBuilder();
     
            builder.GetRequestNode()
		        .Dial("(555)555-5555", hideCallerId:false, dialMusic:"dial music", confirmSound:"dial confirm sound",                              digitsMatch:"ww12w3221", record:false, recordDirection:RecordDirectionEnum.@out)
		            .StartInner()
		            .Sip("username@domain.com", "username", "password")
		            .EndInner()
		        .Gather()
		            .StartInner()
		            .Say(language: LanguageEnum.en, loop: 3, value: "Welcome to Zang!", voice: VoiceEnum.female)
		            .Pause(length: 2)
		            .EndInner()
		        .Hangup(schedule: 4, reason: HangupReasonEnum.rejected);

            var result = builder.Build();
                           
            Console.WriteLine(result);
        }
        catch (ZangException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
```

will render

```xml
<Response>
  <Dial dialMusic="dial music" confirmSound="dial confirm sound" digitsMatch="ww12w3221" hideCallerId="False" record="False" recordDirection="out">
    <Sip username="username" password="password">username@domain.com</Sip>
  </Dial>
  <Gather>
    <Say loop="3" voice="female" language="en">Welcome to Zang!</Say>
    <Pause length="2" />
  </Gather>
  <Hangup schedule="4" reason="rejected" />
</Response>
```
