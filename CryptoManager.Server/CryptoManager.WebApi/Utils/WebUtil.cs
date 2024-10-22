namespace CryptoManager.WebApi.Utils
{
    public static class WebUtil
    {
        public const string ADMINISTRATOR_ROLE_NAME = "Administrator";
        public static string JwtKeyName { get; set; }
        public static string FacebookAppId { get; set; }
        public static string FacebookAppSecret { get; set; }
        public static string SuperUserEmail { get; set; }
        public static string GoogleAppId { get; set; }
        public static string GoogleAppSecret { get; set; }
    }
}
