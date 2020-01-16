using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace AvayaCPaaS.Exceptions
{
    /// <summary>
    /// Exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class CPaaSException : Exception
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [JsonProperty(PropertyName = "code")]
        public long Code { get; set; }

        /// <summary>
        /// Gets or sets the more information.
        /// </summary>
        /// <value>
        /// The more information.
        /// </value>
        [JsonProperty(PropertyName = "more_info")]
        public string MoreInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CPaaSException"/> class.
        /// </summary>
        [JsonConstructor]
        public CPaaSException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CPaaSException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CPaaSException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CPaaSException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="status">The status.</param>
        /// <param name="code">The code.</param>
        /// <param name="moreInfo">The more information.</param>
        public CPaaSException(string message, int status, long code, string moreInfo)
            : base(message)
        {
            this.Status = status;
            this.Code = code;
            this.MoreInfo = moreInfo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CPaaSException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        public CPaaSException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                this.Status = info.GetInt32("Status");
                this.Code = info.GetInt64("Code");
                this.MoreInfo = info.GetString("MoreInfo");
            }
        }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
        /// </PermissionSet>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info != null)
            {
                info.AddValue("Status", this.Status);
                info.AddValue("Code", this.Code);
                info.AddValue("MoreInfo", this.MoreInfo);
            }

            base.GetObjectData(info, context);
        }
    }
}
