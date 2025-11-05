namespace UserManagement.Domain.Options
{
    public sealed class OTPOptions
    {
        public int CodeLength { get; set; }
        public int ExpirationTimeInMinutes { get; set; }
    }
}
