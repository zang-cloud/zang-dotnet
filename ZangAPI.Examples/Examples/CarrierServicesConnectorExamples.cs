using System;
using ZangAPI.Configuration;
using ZangAPI.Exceptions;

namespace ZangAPI.Examples.Examples
{
    /// <summary>
    /// Examples of using Zang service to work with carrier services
    /// </summary>
    public class CarrierServicesConnectorExamples
    {
        private const string AccountSid = "AccountSid";
        private const string AuthToken = "AuthToken";

        private readonly ZangService service = new ZangService(new ZangConfiguration(AccountSid, AuthToken));

        /// <summary>
        /// Example of carrier lookup
        /// </summary>
        public void CarrierLookup()
        {
            try
            {
                // Carrier lookup using carrier services connector
                var carrierLookup = service.CarrierServicesConnector.CarrierLookup("+1234");
                Console.WriteLine(carrierLookup.Price);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        ///  Example of listing carrier lookups
        /// </summary>
        public void CarrierLookupList()
        {
            try
            {
                // List carrier lookups using carrier services connector
                var carrierLookups = service.CarrierServicesConnector.CarrierLookupList();
                Console.WriteLine(carrierLookups.Total);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        ///  Example of cnam lookup
        /// </summary>
        public void CnamLookup()
        {
            try
            {
                // Cnam lookup using carrier services connector
                var cnamLookup = service.CarrierServicesConnector.CnamLookup("+1234");
                Console.WriteLine(cnamLookup.Price);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing cnam lookups
        /// </summary>
        public void CnamLookupList()
        {
            try
            {
                // List cnam lookups using carrier services connector
                var cnamLookups = service.CarrierServicesConnector.CnamLookupList();
                Console.WriteLine(cnamLookups.Total);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        ///  Example of bna lookup
        /// </summary>
        public void BnaLookup()
        {
            try
            {
                // Bna lookup using carrier services connector
                var cnamLookup = service.CarrierServicesConnector.BnaLookup("+1234");
                Console.WriteLine(cnamLookup.Price);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Example of listing bna lookups
        /// </summary>
        public void BnaLookupList()
        {
            try
            {
                // List bna lookups using carrier services connector
                var cnamLookups = service.CarrierServicesConnector.BnaLookupList();
                Console.WriteLine(cnamLookups.Total);
            }
            catch (ZangException e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
