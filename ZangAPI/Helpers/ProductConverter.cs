using System;
using Newtonsoft.Json;
using ZangAPI.Model.Enums;

namespace ZangAPI.Helpers
{
    /// <summary>
    /// Enum products converter
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public class ProductConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var messageTransportResponseProduct = (Product)value;

            switch (messageTransportResponseProduct)
            {
                case Product.OUTBOUND_CALL:
                    writer.WriteValue("1");
                    break;
                case Product.INBOUND_CALL:
                    writer.WriteValue("2");
                    break;
                case Product.OUTBOUND_SMS:
                    writer.WriteValue("3");
                    break;
                case Product.INBOUND_SMS:
                    writer.WriteValue("4");
                    break;
                case Product.OUTBOUND_SIP:
                    writer.WriteValue("5");
                    break;
                case Product.INBOUND_SIP:
                    writer.WriteValue("6");
                    break;
                case Product.RECORDING:
                    writer.WriteValue("7");
                    break;
                case Product.RECURRING_DID:
                    writer.WriteValue("8");
                    break;
                case Product.RECURRING_DID_PREMIUM:
                    writer.WriteValue("9");
                    break;
                case Product.TRANSCRIPTION_AUTO:
                    writer.WriteValue("12");
                    break;
                case Product.TRANSCRIPTION_HYBRID:
                    writer.WriteValue("14");
                    break;
                case Product.RECURRING_INBOUND_CHANNEL:
                    writer.WriteValue("17");
                    break;
                case Product.INBOUND_CALL_CHANNEL:
                    writer.WriteValue("18");
                    break;
                case Product.CNAM_DIP:
                    writer.WriteValue("19");
                    break;
                case Product.CARRIER_LOOKUP:
                    writer.WriteValue("20");
                    break;
                case Product.OUTBOUND_CALL_SPOOFED:
                    writer.WriteValue("21");
                    break;
                case Product.INBOUND_CALL_CHANNEL_OVERAGE:
                    writer.WriteValue("22");
                    break;
                case Product.RECURRING_DID_UNBLOCK:
                    writer.WriteValue("23");
                    break;
                case Product.INBOUND_CALL_UNBLOCKED:
                    writer.WriteValue("24");
                    break;
                case Product.INBOUND_CALL_FORWARDED_FROM:
                    writer.WriteValue("25");
                    break;
            }
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var enumString = (string)reader.Value;

            switch (enumString)
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

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
