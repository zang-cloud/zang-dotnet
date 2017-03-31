using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockHttpServer;
using Newtonsoft.Json;
using ZangAPI.Configuration;
using ZangAPI.Model.Enums;

namespace ZangAPI.Tests
{
    /// <summary>
    /// Tests for SmsConncector
    /// </summary>
    [TestClass]
    public class SmsConnectorTests
    {

        [TestMethod]
        public void SmsConnectorSendSms()
        {
            using (new MockServer(21513, "/Accounts/{accountSid}/SMS/Messages.json", (req, rsp, prm) =>
            {
                // Check http method
                if (req.HttpMethod.Equals("GET")) throw new ArgumentException();
                              
                // Check parameter equality
                this.CheckParametersEquality("sms.json", "bodyParams", req);
                this.CheckParametersEquality("sms.json", "queryParams", req);

                // Define server response
                var assembly = Assembly.GetExecutingAssembly();
                var streamReader = new StreamReader(assembly.GetManifestResourceStream("ZangAPI.Tests.sms.json"));
                var smsJson = streamReader.ReadToEnd();

                var buffer = Encoding.ASCII.GetBytes(smsJson);
                rsp.Content(buffer);
            }))
            {

                var configuration = new ZangConfiguration("ACe1889084056167d57a944486a50ceb46",
                    "ae26e76668654d25b7ef5d39ee9dcd75");
                configuration.BaseUrl = "http://localhost:21513/";

                var service = new ZangService(configuration);
                var message = service.SmsConnector.SendSms("+123456", "test from c#", "+654321",
                    "callback.url", HttpMethod.GET);

                Assert.AreEqual("+123456", message.To);
                Assert.AreEqual("test from c#", message.Body);
                Assert.AreEqual("+654321", message.From);
                Assert.AreEqual(SmsDirection.OUTBOUND_API, message.Direction);
                Assert.AreEqual(Convert.ToDecimal(0.0616), message.Price);
            }
        }

        /// <summary>
        /// Checks the parameters equality.
        /// </summary>
        /// <param name="jsonFileName">Name of the json file.</param>
        /// <param name="paramsType">Type of the parameters.</param>
        /// <param name="request">The request.</param>
        /// <exception cref="System.ArgumentException"></exception>
        private void CheckParametersEquality(string jsonFileName, string paramsType, HttpListenerRequest request)
        {
            // Get body parameters from json file
            var paramsFromJsonDict = GetParamsFromJsonFile(jsonFileName, paramsType);

            if (paramsFromJsonDict.Count != 0)
            {
                Dictionary<string, string> paramsFromRequestDict = new Dictionary<string, string>();
                switch (paramsType)
                {
                    case "bodyParams":
                        paramsFromRequestDict = GetBodyParamsFromRequest(request);
                        break;
                    case "queryParams":
                        paramsFromRequestDict = GetQueryParamsFromRequest(request);
                        break;
                }

                // Compare parameters
                if (!AreDictionaryEqual(paramsFromJsonDict, paramsFromRequestDict))
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// Gets the parameters from json file.
        /// </summary>
        /// <param name="jsonFileName">Name of the json file.</param>
        /// <param name="paramsType">Type of the parameters.</param>
        /// <returns>Returns parameters (body or query) from json file</returns>
        private Dictionary<string, string> GetParamsFromJsonFile(string jsonFileName, string paramsType)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var streamReader = new StreamReader(assembly.GetManifestResourceStream($"ZangAPI.Tests.{jsonFileName}"));
            var objectJson = streamReader.ReadToEnd();

            // Get json request from json file
            var jsonRequest = JsonConvert.DeserializeObject<JsonRequest>(objectJson);

            var paramsJson = new List<Parameter>();

            // Get parameters depending on parameter type
            switch (paramsType)
            {
                case "bodyParams":
                    paramsJson = jsonRequest.BodyParams;
                    break;
                case "queryParams":
                    paramsJson = jsonRequest.QueryParams;
                    break;
            }

            return paramsJson.ToDictionary(value => value.Name, value => value.Value);
        }

        /// <summary>
        /// Gets the body parameters from request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Returns body parameters</returns>
        public static Dictionary<string, string> GetBodyParamsFromRequest(HttpListenerRequest request)
        {
            var data = GetRequestPostData(request);

            return data.Split('&')
                .Select(x => x.Split('='))
                .ToDictionary(x => x[0], x => Uri.UnescapeDataString(x[1]));
        }

        /// <summary>
        /// Gets the query parameters from request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Returns query parameters</returns>
        public static Dictionary<string, string> GetQueryParamsFromRequest(HttpListenerRequest request)
        {
            var qsArray = request.QueryString.AllKeys
                    .Select(key => new { Key = key.ToString(), Value = Uri.UnescapeDataString(request.QueryString[key.ToString()]) })
                    .ToArray();

           return qsArray.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /// <summary>
        /// Gets the request post data.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Returns request post data</returns>
        public static string GetRequestPostData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                return null;
            }
            using (Stream body = request.InputStream)
            {
                using (StreamReader reader = new System.IO.StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Ares the dictionary equal.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="otherDictionary">The other dictionary.</param>
        /// <returns>Returns true if dictionaries have equal keys and values, false otherwise</returns>
        public static bool AreDictionaryEqual<TKey, TValue>(Dictionary<TKey, TValue> dictionary, Dictionary<TKey, TValue> otherDictionary)
        {
            return (otherDictionary ?? new Dictionary<TKey, TValue>())
                .OrderBy(kvp => kvp.Key)
                .SequenceEqual((dictionary ?? new Dictionary<TKey, TValue>())
                                   .OrderBy(kvp => kvp.Key));
        }
    }
}