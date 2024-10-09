using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using nona.Models;
//using RestClient.Net;
//using RestClient.Net.Abstractions.Extensions;

namespace nona.Services
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;
        public EmailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> SendMail(ContactModel model)
        {
            try
            {
                var url = "https://api.pepipost.com/v5/mail/send";
                var jsonObject = JsonConvert.SerializeObject(GetPepipostModel(model));
                var stringContent = new StringContent(jsonObject);
                AddHeaderApi(_httpClient, url, "bdf16d57311e1ead6bd0c332210e63de");
                var response = await _httpClient.PostAsync("", stringContent);
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        return true;
                    }

                    return true;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return false;
        }

        private HttpClient AddHeaderApi(HttpClient client, string url, string token)
        {
            client.BaseAddress = new Uri(url);
            //client.DefaultRequestHeaders.Add("Content-Type", "application/json;");
            client.DefaultRequestHeaders.Add("api_key", token);
            return client;
        }

        private PepipostModel GetPepipostModel(ContactModel model)
        {
            var pepipostModel = new PepipostModel()
            {
                subject = model.Subject,
                from = new From()
                {
                    email = model.Email,
                    name = model.Name
                }
            };
            Content[] contents = { new Content() { type = "html", value = model.Message }, };
            pepipostModel.content = contents;
            To[] toMail = { new To() { email = "noname9xnd@gmail.com", name = "nona" } };
            Personalization[] personalizations = { new Personalization() { to = toMail } };
            pepipostModel.personalizations = personalizations;

            return pepipostModel;
        }
    }
}
