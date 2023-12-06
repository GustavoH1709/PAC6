using System.Text.Json;
using Newtonsoft.Json.Linq;
using PAC6.API.DTO;
using PAC6.API.Interfaces;
using PAC6.API.Providers;
using PAC6.API.Requests;
using System.Net;
using PAC6.API.Validators;

namespace PAC6.API.Application
{
    public class CreateParametersApplication : ICreateParametersApplication
    {
        private readonly FirebaseConnectionProvider _firebase;

        public CreateParametersApplication()
        {
            _firebase = new();
        }

        public async Task<bool> Handle(CreateParametersCommand command)
        {
            if (!KeyValidator.IsValid(command.API_KEY))
            {
                return false;
            }

            var check = _firebase.Connection.Get("Parameters/");

            if (check.Body == "null")
            {
                var response = _firebase.Connection.Push("Parameters/", new ParametersDTO()
                {
                    MaxHumidade = command.MaxHumidade,
                    MaxTemperatura = command.MaxTemperatura,
                    MinHumidade = command.MinHumidade,
                    MinTemperatura = command.MinTemperatura,
                    CreatedAt = DateTime.Now,
                });

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            else
            {
                var parametersParsed = JsonSerializer.Deserialize<Dictionary<string, ParametersDTO>>(check.Body);

                if (parametersParsed == null)
                {
                    return false;
                }

                var first = parametersParsed.FirstOrDefault();

                var response = _firebase.Connection.Update("Parameters/", new Dictionary<string, ParametersDTO>()
                {
                   { 
                        first.Key, 
                        new ParametersDTO() 
                        { 
                            MaxHumidade = command.MaxHumidade,
                            MaxTemperatura = command.MaxTemperatura,
                            MinHumidade = command.MinHumidade,
                            MinTemperatura = command.MinTemperatura, 
                            CreatedAt = DateTime.Now 
                        } 
                    }
                });

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }

            return await Task.FromResult(false);
        }
    }
}
