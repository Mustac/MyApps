using Mailjet.Client.Resources;
using System.Reflection.Metadata.Ecma335;

namespace MyAppServer
{
    public class JwtSecrets
    {
        private static string key;
        public static string Key
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                    key = Environment.GetEnvironmentVariable("JwtKey");

                return key;
            }
        }

        private static string audience;
        public static string Audience
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                    audience = Environment.GetEnvironmentVariable("JwtAudience");

                return audience;
            }
        }

        private static string issuer;
        public static string Issuer { 
            get
            {
                if (string.IsNullOrEmpty(key))
                    issuer = Environment.GetEnvironmentVariable("JwtIssuer");

                return issuer;
            }
        }
    }
}
