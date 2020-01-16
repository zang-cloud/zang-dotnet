using System;
using AvayaCPaaS.Configuration;
using AvayaCPaaS.Exceptions;

namespace AvayaCPaaS.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with accounts
    /// </summary>
    public class AccountsConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly CPaaSService service = new CPaaSService(new APIConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of viewing account
        /// </summary>
        public void ViewAccount()
        {
            try
            {
                // View account using accounts connector
                var account = service.AccountsConnector.ViewAccount(AccountSid);
                Console.WriteLine(account.AccountBalance);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of updating account
        /// </summary>
        public void UpdateAccount()
        {
            try
            {
                // Update account using accounts connector
                var account = service.AccountsConnector.UpdateAccount("friendlyName");
                Console.WriteLine(account.FriendlyName);
            }
            catch (CPaaSException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
