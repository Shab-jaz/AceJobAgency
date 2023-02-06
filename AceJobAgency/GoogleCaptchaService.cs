using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AceJobAgency
{
    public class GoogleCaptchaService
    {
        private GoogleCaptchaConfig _config;

        public GoogleCaptchaService(IOptions<GoogleCaptchaConfig> config)
        {
            _config = config.Value;
        }

        public virtual async Task<GoogleRespo> ResVer(string _Token)
        {
            GoogleCaptchaData _MyData = new GoogleCaptchaData
            {
                response = _Token,
                secret = _config.SecretKey,

            };
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?=secret{_MyData.secret}&response={_MyData.response} ");
            var capresp = JsonConvert.DeserializeObject<GoogleRespo>(response);
            return capresp;
        }
    }
    public class GoogleCaptchaData
    {
        public string response { get; set; }//token
        public string secret { get; set; }
    }
    public class GoogleRespo
    {
        public bool success { get; set; }
        public double score { get; set; }
        public string action { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }

    }
}
