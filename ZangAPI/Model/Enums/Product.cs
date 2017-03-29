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
}
