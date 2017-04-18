namespace ZangAPI.Model.Enums
{
    /// <summary>
    /// Auth type
    /// </summary>
    public enum AuthType
    {
        /// <summary>
        /// The ip acl
        /// </summary>
        IP_ACL,

        /// <summary>
        /// The credential list
        /// </summary>
        CREDENTIAL_LIST, 

        //todo nema u dokumentaciji
        NO_TRAFFIC, 

        //todo nema u dokumentaciji
        IP_AND_CREDENTIAL
    }
}
