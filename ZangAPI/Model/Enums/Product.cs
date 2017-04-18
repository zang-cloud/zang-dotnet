﻿using System.Collections.Generic;

namespace ZangAPI.Model.Enums
{
    /// <summary>
    /// Product
    /// </summary>
    public enum Product
    {
        OUTBOUND_CALL,
        INBOUND_CALL,
        OUTBOUND_SMS,
        INBOUND_SMS,
        OUTBOUND_SIP,
        INBOUND_SIP,
        RECORDING,
        RECURRING_DID,
        RECURRING_DID_PREMIUM,
        TRANSCRIPTION_AUTO,
        TRANSCRIPTION_HYBRID,
        RECURRING_INBOUND_CHANNEL,
        INBOUND_CALL_CHANNEL,
        CNAM_DIP,
        CARRIER_LOOKUP,
        OUTBOUND_CALL_SPOOFED,
        INBOUND_CALL_CHANNEL_OVERAGE,
        RECURRING_DID_UNBLOCK,
        INBOUND_CALL_UNBLOCKED,
        INBOUND_CALL_FORWARDED_FROM,
        UNKNOWN
    }

    public static class ProductStringConverter
    {
        public static Product? GetProduct(string product)
        {
            var dict = new Dictionary<string, Product>()
            {
                { "Outbound Call", Product.OUTBOUND_CALL},
                { "Inbound Call", Product.INBOUND_CALL},
                { "Outbound SMS", Product.OUTBOUND_SMS},
                { "Inbound SMS", Product.INBOUND_SMS},
                { "Outbound SIP", Product.OUTBOUND_SIP},
                { "Inbound SIP", Product.INBOUND_SIP},
                { "Recording", Product.RECORDING},
                { "Recurring DID", Product.RECURRING_DID},
                { "Recurring DID (Premium)", Product.RECURRING_DID_PREMIUM},
                { "Transcription (Auto)", Product.TRANSCRIPTION_AUTO},
                { "Transcription (Hybrid)", Product.TRANSCRIPTION_HYBRID},
                { "Recurring Inbound Channel", Product.RECURRING_INBOUND_CHANNEL},
                { "Inbound Call (Channel)", Product.INBOUND_CALL_CHANNEL},
                { "CNAM Dip", Product.CNAM_DIP},
                { "Carrier Lookup", Product.CARRIER_LOOKUP},
                { "Outbound Call (Spoofed)", Product.OUTBOUND_CALL_SPOOFED},
                { "Inbound Call (Channel Overage)", Product.INBOUND_CALL_CHANNEL_OVERAGE},
                { "Recurring DID Unblock", Product.RECURRING_DID_UNBLOCK},
                { "Inbound Call Unblocked", Product.INBOUND_CALL_UNBLOCKED},
                { "Inbound Call Forwarded From", Product.INBOUND_CALL_FORWARDED_FROM}
            };

            return dict[product];
        }

        public static string GetProduct(Product product)
        {
            var dict = new Dictionary<Product, string>()
            {
                { Product.OUTBOUND_CALL, "1"},
                { Product.INBOUND_CALL, "2"},
                { Product.OUTBOUND_SMS, "3"},
                { Product.INBOUND_SMS, "4"},
                { Product.OUTBOUND_SIP, "5"},
                { Product.INBOUND_SIP, "6"},
                { Product.RECORDING, "7"},
                { Product.RECURRING_DID, "8"},
                { Product.RECURRING_DID_PREMIUM, "9"},
                { Product.TRANSCRIPTION_AUTO, "12"},
                { Product.TRANSCRIPTION_HYBRID, "14"},
                { Product.RECURRING_INBOUND_CHANNEL, "17"},
                { Product.INBOUND_CALL_CHANNEL, "18"},
                { Product.CNAM_DIP, "19"},
                { Product.CARRIER_LOOKUP, "20"},
                { Product.OUTBOUND_CALL_SPOOFED, "21"},
                { Product.INBOUND_CALL_CHANNEL_OVERAGE, "22"},
                { Product.RECURRING_DID_UNBLOCK, "23"},
                { Product.INBOUND_CALL_UNBLOCKED, "24"},
                { Product.INBOUND_CALL_FORWARDED_FROM, "25"},
            };

            return dict[product];
        }

        public static Product? GetProduct(int productId)
        {
            var dict = new Dictionary<int, Product>()
            {
                { 1, Product.OUTBOUND_CALL },
                { 2, Product.INBOUND_CALL},
                { 3, Product.OUTBOUND_SMS},
                { 4, Product.INBOUND_SMS},
                { 5, Product.OUTBOUND_SIP},
                { 6, Product.INBOUND_SIP},
                { 7, Product.RECORDING},
                { 8, Product.RECURRING_DID},
                { 9, Product.RECURRING_DID_PREMIUM},
                { 12, Product.TRANSCRIPTION_AUTO},
                { 14, Product.TRANSCRIPTION_HYBRID},
                { 17, Product.RECURRING_INBOUND_CHANNEL},
                { 18, Product.INBOUND_CALL_CHANNEL},
                { 19, Product.CNAM_DIP},
                { 20, Product.CARRIER_LOOKUP},
                { 21, Product.OUTBOUND_CALL_SPOOFED},
                { 22, Product.INBOUND_CALL_CHANNEL_OVERAGE},
                { 23, Product.RECURRING_DID_UNBLOCK},
                { 24, Product.INBOUND_CALL_UNBLOCKED},
                { 25, Product.INBOUND_CALL_FORWARDED_FROM},
            };

            return dict[productId];
        }
    }
}
