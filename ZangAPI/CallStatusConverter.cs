using System;
using Newtonsoft.Json;
using ZangAPI.Model.Enums;

namespace ZangAPI
{
    //todo visak
    public class CallStatusConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            CallStatus messageTransportResponseStatus = (CallStatus) value;

            switch (messageTransportResponseStatus)
            {
                case CallStatus.PRE_QUEUED:
                    writer.WriteValue("pre-queued");
                    break;
                case CallStatus.QUEUED:
                    writer.WriteValue("queued");
                    break;
                case CallStatus.RINGING:
                    writer.WriteValue("ringing");
                    break;
                case CallStatus.IN_PROGRESS:
                    writer.WriteValue("in-progress");
                    break;
                case CallStatus.COMPLETED:
                    writer.WriteValue("completed");
                    break;
                case CallStatus.FAILED:
                    writer.WriteValue("failed");
                    break;
                case CallStatus.BUSY:
                    writer.WriteValue("busy");
                    break;
                case CallStatus.NO_ANSWER:
                    writer.WriteValue("no-answer");
                    break;
                case CallStatus.UNKNOWN:
                    writer.WriteValue("unknown");
                    break;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var enumString = (string)reader.Value;

            switch (enumString)
            {
                case "pre-queued":
                    return CallStatus.PRE_QUEUED;
                case "queued":
                    return CallStatus.QUEUED;
                case "ringing":
                    return CallStatus.RINGING;
                case "in-progress":
                    return CallStatus.IN_PROGRESS;
                case "completed":
                    return CallStatus.COMPLETED;
                case "failed":
                    return CallStatus.FAILED;
                case "busy":
                    return CallStatus.BUSY;
                case "no-answer":
                    return CallStatus.NO_ANSWER;
                case "unknown":
                    return CallStatus.UNKNOWN;
            }

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
