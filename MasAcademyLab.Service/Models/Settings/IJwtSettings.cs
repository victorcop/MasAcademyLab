namespace MasAcademyLab.Service.Models.Settings
{
    public interface IJwtSettings
    {
        /// <summary>
        /// JWT Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// JWT Issuer
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// JWT Audience
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Expiry Duration Minutes
        /// </summary>
        public double ExpiryDurationMinutes { get; set; }
    }
}
