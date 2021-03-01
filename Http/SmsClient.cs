using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using MailSender.Data;
using MailSender.Data.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MailSender.Http.Models;


namespace MailSender.Http
{
    public class SmsClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private readonly ISeriLogService _seriLogService;

        public SmsClient(HttpClient client, IConfiguration config, ISeriLogService seriLogService)
        {
            _client = client;
            _config = config;
            _seriLogService = seriLogService;
            _client.Timeout = new TimeSpan(0, 0, 30);
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<SmsResponseModel> SendSms(SmsRequestModel requestModel)
        {
            var responseModel = new SmsResponseModel();
            var address = "http://195.46.130.73:9091/";
            var serializedRequestModel = JsonConvert.SerializeObject(requestModel);
            var request = new HttpRequestMessage(HttpMethod.Post, address + "api/SMS/SendSms")
            {
                Content = new StringContent(serializedRequestModel)
            };
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //_seriLogService.Info("PaymentClient>Payment", model: requestModel);
            try
            {
                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                responseModel = stream.ReadAndDeserializeFromJson<SmsResponseModel>();
                _seriLogService.Info("PaymentClient>Payment", model: responseModel);
            }
            catch (Exception ex)
            {
                _seriLogService.Error("PaymentClient>Payment", ex, model: requestModel);
            }


            return responseModel;
        }

        internal SmsRequestModel GenerateSmsModel(string phone, string link, string name)
        {
            //throw new NotImplementedException();
            SmsRequestModel model = new SmsRequestModel();

            var strBody =

                          $"Sn.{name} " +
                          "Oyak Rahat TSS Kayıt ve Ödeme bilgilerinize alttaki linkten ulaşabilirsiniz. " +
                          $"{link} " +
                          "Saygılarımızla, " +
                          "OYAK Grup Sigorta A.Ş.";


            model.messageText = strBody;
            // refine phone
            model.numbers.Add(RefinePhone(phone));
            model.isTurkish = true;
            model.date = DateTime.Now;
            return model;
        }
        private static string RefinePhone(string rawPhone)
        {
            var refined = rawPhone.Replace("(", "");
            refined = refined.Replace(")", "");
            refined = refined.Replace("-", "");
            refined = refined.Replace(" ", "");
            refined = refined.Trim();
            return refined;
        }
    }
}
