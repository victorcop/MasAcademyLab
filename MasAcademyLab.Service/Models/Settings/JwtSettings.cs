namespace MasAcademyLab.Service.Models.Settings
{
    public class JwtSettings : IJwtSettings
    {
        ///<inheritdoc/>
        public string Key { get; set; }

        ///<inheritdoc/>
        public string Issuer { get; set; }

        ///<inheritdoc/>
        public string Audience { get; set; }

        ///<inheritdoc/>
        public double ExpiryDurationMinutes { get; set; }
    }
}
