using System.Text.Json;
using Newtonsoft.Json.Linq;
using PAC6.API.DTO;
using PAC6.API.Interfaces;
using PAC6.API.Providers;
using PAC6.API.Requests;
using System.Net;

namespace PAC6.API.Application
{
    public class CreateParametersApplication : ICreateParametersApplication
    {
        private readonly FirebaseConnectionProvider _firebase;

        public CreateParametersApplication(FirebaseConnectionProvider firebase)
        {
            _firebase = firebase;
        }

        public async Task<bool> Handle(CreateParametersCommand command)
        {
            var check = _firebase.Connection.Get("Parameters/");


            if (check.Body == "null")
            {
                var response = await _firebase.Connection.PushAsync("Parameters/", new ParametersDTO()
                {
                    Max = command.Max,
                    Min = command.Min,
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

                var first = parametersParsed.FirstOrDefault();

                var response = await _firebase.Connection.UpdateAsync("Parameters/", new Dictionary<string, ParametersDTO>()
                {
                   { first.Key, new ParametersDTO() { Max = command.Max,  Min = command.Min, CreatedAt = DateTime.Now } }
                });

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
