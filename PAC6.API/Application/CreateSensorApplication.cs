using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using PAC6.API.DTO;
using PAC6.API.Interfaces;
using PAC6.API.Providers;
using PAC6.API.Requests;
using System.Net;
using System.Text.Json;

namespace PAC6.API.Application
{
    public class CreateSensorApplication : ICreateSensorApplication
    {
        private readonly FirebaseConnectionProvider _firebase;
        private readonly IEmailProvider _emailProvider;
        public CreateSensorApplication(IEmailProvider emailProvider)
        {
            _firebase = new();
            _emailProvider = emailProvider;
        }

        public async Task<bool> Handle(CreateSensorCommand command)
        {
            var response = await _firebase.Connection.PushAsync("Data/", new DataDTO()
            {
                Humidity = command.Humidity,
                Temperature = command.Temperature,
                CreatedAt = DateTime.Now,
            });

            if(response != null && response.StatusCode == HttpStatusCode.OK)
            {
                var getEmails = _firebase.Connection.Get("Emails/");

                if(getEmails.Body != "null")
                {
                    List<string> emails = JsonSerializer
                                          .Deserialize<Dictionary<string, EmailDTO>>(getEmails.Body)
                                          .Values
                                          .ToList()
                                          .Select(x => x.Email)
                                          .ToList();

                    _emailProvider.Handle(new() { Body = "", Subject = "" }, emails);
                }

                return true;
            }

            return false;
        }

        private void EnviaEmail(List<string> emails, string Body, string Subject)
        {

        }
    }
}
