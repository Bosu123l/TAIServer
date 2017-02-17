using System.Collections.Generic;

namespace TAI.Utils.Helpers
{
    public static class JWTHelper
    {
        private static string secretKey = "TUTAJ_USTAW_MACHINE_KEY_LUB_INNY_BEZPIECZNY_KLUCZ";

        public static string EncodeToken(Dictionary<string, object> payload)
        {
            return JWT.JsonWebToken.Encode(payload, secretKey, JWT.JwtHashAlgorithm.HS256);
        }

        public static Dictionary<string, object> DecodeToken(string token)
        {
            return JWT.JsonWebToken.DecodeToObject<Dictionary<string, object>>(token, secretKey);
        }
    }
}