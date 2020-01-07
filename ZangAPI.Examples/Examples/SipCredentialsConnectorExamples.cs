using System;
using ZangAPI.Configuration;
using ZangAPI.Exceptions;

namespace ZangAPI.Examples.Examples
{
    /// <summary>
    /// Examples of using Avaya CPaaS service to work with sip credentials and credential lists
    /// </summary>
    public class SipCredentialsConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly ZangService service = new ZangService(new ZangConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of viewing credential
        /// </summary>
        public void ViewCredential()
        {
            try
            {
                // View credential using sip credentials connector
                var credential = service.SipCredentialsConnector.ViewCredential("TestCredentialsListSid", "TestCredentialSid");
                Console.WriteLine(credential.FriendlyName);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing credentials
        /// </summary>
        public void ListCredentials()
        {
            try
            {
                // List credentials using sip credentials connector
                var credentials = service.SipCredentialsConnector.ListCredentials("TestCredentialsListSid");
                Console.WriteLine(credentials.Total);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of creating credential
        /// </summary>
        public void CreateCredential()
        {
            try
            {
                // Create credential using sip credentials connector
                var credential = service.SipCredentialsConnector.CreateCredential("TestCredentialsListSid", "username", "password");
                Console.WriteLine(credential.Username);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of updating credential
        /// </summary>
        public void UpdateCredential()
        {
            try
            {
                // Update credential using sip credentials connector
                var credential = service.SipCredentialsConnector.UpdateCredential("TestCredentialsListSid", "TestCredentialSid",
                    "password");
                Console.WriteLine(credential.FriendlyName);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of deleting credential
        /// </summary>
        public void DeleteCredential()
        {
            try
            {
                // Delete credential using sip credentials connector
                var credential = service.SipCredentialsConnector.DeleteCredential("TestCredentialsListSid", "TestCredentialSid");
                Console.WriteLine(credential.Sid);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of viewing credentials list
        /// </summary>
        public void ViewCredentialsList()
        {
            try
            {
                // View credentials list using sip credentials connector
                var credentialsList = service.SipCredentialsConnector.ViewCredentialsList("TestCredentialsListSid");
                Console.WriteLine(credentialsList.CredentialsCount);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing credentials lists
        /// </summary>
        public void ListCredentialsLists()
        {
            try
            {
                // List credentials lists using sip credentials connector
                var credentialsLists = service.SipCredentialsConnector.ListCredentialsLists();
                Console.WriteLine(credentialsLists.Total);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of creating credentials list
        /// </summary>
        public void CreateCredentialsList()
        {
            try
            {
                // Create credentials list using sip credentials connector
                var credentialsList = service.SipCredentialsConnector.CreateCredentialsList("CredentialsListFriendlyName");
                Console.WriteLine(credentialsList.FriendlyName);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of updating credentials list
        /// </summary>
        public void UpdateCredentialsList()
        {
            try
            {
                // Update credentials list using sip credentials connector
                var credentialsList = service.SipCredentialsConnector.UpdateCredentialsList("TestCredentialsListSid",
                    "CredentialsListNewFriendlyName");
                Console.WriteLine(credentialsList.FriendlyName);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of deleting credentials list
        /// </summary>
        public void DeleteCredentialsList()
        {
            try
            {
                // Delete credentials list using sip credentials connector
                var credentialsList = service.SipCredentialsConnector.DeleteCredentialsList("TestCredentialsListSid");
                Console.WriteLine(credentialsList.Sid);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
