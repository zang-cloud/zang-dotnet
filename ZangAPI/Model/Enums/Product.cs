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
            switch (product)
            {
                case "Outbound Call":
                    return Product.OUTBOUND_CALL;
                case "Inbound Call":
                    return Product.INBOUND_CALL;
                case "Outbound SMS":
                    return Product.OUTBOUND_SMS;
                case "Inbound SMS":
                    return Product.INBOUND_SMS;
                case "Outbound SIP":
                    return Product.OUTBOUND_SIP;
                case "Inbound SIP":
                    return Product.INBOUND_SIP;
                case "Recording":
                    return Product.RECORDING;
                case "Recurring DID":
                    return Product.RECURRING_DID;
                case "Recurring DID (Premium)":
                    return Product.RECURRING_DID_PREMIUM;
                case "Transcription (Auto)":
                    return Product.TRANSCRIPTION_AUTO;
                case "Transcription (Hybrid)":
                    return Product.TRANSCRIPTION_HYBRID;
                case "Recurring Inbound Channel":
                    return Product.RECURRING_INBOUND_CHANNEL;
                case "Inbound Call (Channel)":
                    return Product.INBOUND_CALL_CHANNEL;
                case "CNAM Dip":
                    return Product.CNAM_DIP;
                case "Carrier Lookup":
                    return Product.CARRIER_LOOKUP;
                case "Outbound Call (Spoofed)":
                    return Product.OUTBOUND_CALL_SPOOFED;
                case "Inbound Call (Channel Overage)":
                    return Product.INBOUND_CALL_CHANNEL_OVERAGE;
                case "Recurring DID Unblock":
                    return Product.RECURRING_DID_UNBLOCK;
                case "Inbound Call Unblocked":
                    return Product.INBOUND_CALL_UNBLOCKED;
                case "Inbound Call Forwarded From":
                    return Product.INBOUND_CALL_FORWARDED_FROM;
            }

            return null;
        }

        public static string GetProduct(Product product)
        {
            switch (product)
            {
                case Product.OUTBOUND_CALL:
                    return "1";
                case Product.INBOUND_CALL:
                    return "2";
                case Product.OUTBOUND_SMS:
                    return "3";
                case Product.INBOUND_SMS:
                    return "4";
                case Product.OUTBOUND_SIP:
                    return "5";
                case Product.INBOUND_SIP:
                    return "6";
                case Product.RECORDING:
                    return "7";
                case Product.RECURRING_DID:
                    return "8";
                case Product.RECURRING_DID_PREMIUM:
                    return "9";
                case Product.TRANSCRIPTION_AUTO:
                    return "12";
                case Product.TRANSCRIPTION_HYBRID:
                    return "14";
                case Product.RECURRING_INBOUND_CHANNEL:
                    return "17";
                case Product.INBOUND_CALL_CHANNEL:
                    return "18";
                case Product.CNAM_DIP:
                    return "19";
                case Product.CARRIER_LOOKUP:
                    return "20";
                case Product.OUTBOUND_CALL_SPOOFED:
                    return "21";
                case Product.INBOUND_CALL_CHANNEL_OVERAGE:
                    return "22";
                case Product.RECURRING_DID_UNBLOCK:
                    return "23";
                case Product.INBOUND_CALL_UNBLOCKED:
                    return "24";
                case Product.INBOUND_CALL_FORWARDED_FROM:
                    return "25";
            }

            return null;
        }

        public static Product? GetProduct(int productId)
        {
            switch (productId)
            {
                case 1:
                    return Product.OUTBOUND_CALL;
                case 2:
                    return Product.INBOUND_CALL;
                case 3:
                    return Product.OUTBOUND_SMS;
                case 4:
                    return Product.INBOUND_SMS;
                case 5:
                    return Product.OUTBOUND_SIP;
                case 6:
                    return Product.INBOUND_SIP;
                case 7:
                    return Product.RECORDING;
                case 8:
                    return Product.RECURRING_DID;
                case 9:
                    return Product.RECURRING_DID_PREMIUM;
                case 12:
                    return Product.TRANSCRIPTION_AUTO;
                case 14:
                    return Product.TRANSCRIPTION_HYBRID;
                case 17:
                    return Product.RECURRING_INBOUND_CHANNEL;
                case 18:
                    return Product.INBOUND_CALL_CHANNEL;
                case 19:
                    return Product.CNAM_DIP;
                case 20:
                    return Product.CARRIER_LOOKUP;
                case 21:
                    return Product.OUTBOUND_CALL_SPOOFED;
                case 22:
                    return Product.INBOUND_CALL_CHANNEL_OVERAGE;
                case 23:
                    return Product.RECURRING_DID_UNBLOCK;
                case 24:
                    return Product.INBOUND_CALL_UNBLOCKED;
                case 25:
                    return Product.INBOUND_CALL_FORWARDED_FROM;
            }

            return null;
        }
    }
}
