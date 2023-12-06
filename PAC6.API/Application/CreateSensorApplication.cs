using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FluentEmail;
using PAC6.API.DTO;
using PAC6.API.Interfaces;
using PAC6.API.Providers;
using PAC6.API.Requests;
using PAC6.API.Validators;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
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
            if (!KeyValidator.IsValid(command.API_KEY))
            {
                return false;
            }

            var response = _firebase.Connection.Push("Data/", new DataDTO()
            {
                Humidity = command.Humidity,
                Temperature = command.Temperature,
                CreatedAt = DateTime.Now,
            });

            //var ok = await GravaValor(command.Humidity, command.Temperature);

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            //if(ok)
            {
                var getEmails = _firebase.Connection.Get("Emails/");
                var getParametrosValidacao = _firebase.Connection.Get("Parameters/");

                if (getEmails.Body != "null" && getParametrosValidacao.Body != "null")
                {
                    List<string> emails = (JsonSerializer
                                          .Deserialize<Dictionary<string, EmailDTO>>(getEmails.Body) ?? new())
                                          .Values
                                          .ToList()
                                          .Select(x => x.Email)
                                          .ToList();



                    var parametersParsed = JsonSerializer.Deserialize<Dictionary<string, ParametersDTO>>(getParametrosValidacao.Body) ?? new();

                    var parametrosValidacao = parametersParsed.FirstOrDefault();

                    await ValidaTemperaturaMinima(command.Temperature, emails, parametrosValidacao.Value.MinTemperatura);
                    await ValidaTemperaturaMaxima(command.Temperature, emails, parametrosValidacao.Value.MaxTemperatura);
                    await ValidaHumindadeMinima(command.Humidity, emails, parametrosValidacao.Value.MinHumidade);
                    await ValidaHumindadeMaxima(command.Humidity, emails, parametrosValidacao.Value.MaxHumidade);
                }

                return true;
            }

            return false;
        }

        private async Task<bool> ValidaTemperaturaMinima(float value, List<string> lEmails, float parametro)
        {
            if(value < parametro)
            {
                EmailContentDTO content = new()
                {
                    Subject = "WARNING TEMPERATURA MINIMA",
                    Body = "<h1>WARNING - TEMPERATURA MINIMA</h1>"
                };

                return _emailProvider.Handle(content, lEmails);
            }

            return await Task.FromResult(true);
        }

        private async Task<bool> ValidaTemperaturaMaxima(float value, List<string> lEmails, float parametro)
        {
            if (value > parametro)
            {
                EmailContentDTO content = new()
                {
                    Subject = "WARNING TEMPERATURA MAXIMA",
                    Body = "<h1>WARNING - TEMPERATURA MAXIMA</h1>"
                };

                return _emailProvider.Handle(content, lEmails);
            }

            return await Task.FromResult(true);
        }

        private async Task<bool> ValidaHumindadeMinima(float value, List<string> lEmails, float parametro)
        {
            if (value < parametro)
            {
                EmailContentDTO content = new()
                {
                    Subject = "WARNING HUMINDADE MINIMA",
                    Body = "<h1>WARNING - HUMINDADE MINIMA</h1>"
                };

                return _emailProvider.Handle(content, lEmails);
            }

            return await Task.FromResult(true);
        }

        private async Task<bool> ValidaHumindadeMaxima(float value, List<string> lEmails, float parametro)
        {
            if (value > parametro)
            {
                EmailContentDTO content = new()
                {
                    Subject = "WARNING HUMINDADE MINIMA",
                    Body = "<h1>WARNING - HUMINDADE MINIMA</h1>"
                };

                return _emailProvider.Handle(content, lEmails);
            }

            return await Task.FromResult(true);
        }

        private async Task<bool> GravaValor(float Humidity, float Temperature)
        {
            using HttpClient client = new();

            client.BaseAddress = new Uri("https://pac6-7bde6-default-rtdb.firebaseio.com/Data.json");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("key", "YCeaItSewfIAzNNpIRZEmcPYtQLUYjZBW6FFaAvh");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonSerializer.Serialize(new { Humidity, Temperature, CreatedAt = DateTime.Now });

            var body = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://pac6-7bde6-default-rtdb.firebaseio.com/Data.json", body);

            return response.IsSuccessStatusCode;
        }
    }
}
