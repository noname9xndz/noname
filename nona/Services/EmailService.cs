using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using nona.Models;

namespace nona.Services
{
    public class EmailService : IEmailService
    {
        
        public async Task<bool> SendMail(ContactModel model)
        {
            try
            {
                using(var client = new HttpClient()) 
                {
                    
                    var pepipostModel = new PepipostModel()
                    {
                        subject = model.Subject,
                        content = model.Message,
                        from = new From()
                        {
                            fromEmail = model.Email,
                            fromName = model.Name
                        }
                    };
                    Personalization myEmail = new Personalization() { recipient = "noname9xnd@gmail.com" };
                    pepipostModel.personalizations.Add(myEmail);
                    var url = "https://api.pepipost.com/v2/sendEmail";
                    //todo
                    var stringContent = new StringContent(JsonConvert.SerializeObject(pepipostModel), Encoding.UTF8);
                    AddHeaderApi(client,url, "bdf16d57311e1ead6bd0c332210e63de");
                    var response = await client.PostAsync("", stringContent);
                    if (response != null && response.Content != null)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrWhiteSpace(content))
                        {
                            return true;
                        }

                        return true;
                    }
                };
                
            }
            catch (Exception e)
            {
                
            }

            return false;
        }

        private HttpClient AddHeaderApi(HttpClient client, string url,string token)
        {
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            client.DefaultRequestHeaders.TryAddWithoutValidation("api_key", token);
            return client;
        }
    }
}
