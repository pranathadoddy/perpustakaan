using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Framework.Core
{
    public static class Cryptographer
    {
        #region Constants

        private const string Key = "E77ED353-E5AD-4334-970A-E409BAFC8010";

        #endregion

        #region Public Methods

        public static string JsonWebTokenEncode(Dictionary<string, object> payload)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, Key);
        }

        /// <summary>
        ///     This method will need a try catch for safety since it may throw :
        ///     1. TokenExpiredException
        ///     2. SignatureVerificationException
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Dictionary<string, object> JsonWebTokenDecode(string token)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

            var json = decoder.Decode(token, Key, true);

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }

        #endregion
    }
}
