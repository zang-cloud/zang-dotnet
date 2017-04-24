using System;
using ZangAPI.Configuration;
using ZangAPI.Exceptions;

namespace ZangAPI.Examples.Examples
{
    /// <summary>
    /// Examples of using Zang service to work with accounts
    /// </summary>
    public class AccountsConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly ZangService service = new ZangService(new ZangConfiguration(AccountSid, AuthToken));

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
            catch (ZangException e)
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
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
