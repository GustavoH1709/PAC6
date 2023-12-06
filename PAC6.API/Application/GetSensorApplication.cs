using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PAC6.API.DTO;
using PAC6.API.Interfaces;
using PAC6.API.Providers;
using System.Net;
using System.Text.Json;

namespace PAC6.API.Application
{
    public class GetSensorApplication : IGetSensorApplication
    {
        private readonly FirebaseConnectionProvider _firebase;

        public GetSensorApplication()
        {
            _firebase = new();
        }

        public async Task<List<DataDTO>> Handle()
        {
            var response = _firebase.Connection.Get("Data/");

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Body == null || response.Body == "null")
                    return new List<DataDTO>();

                List<DataDTO> gravados = TryGetList(response.Body);

                return await Task.FromResult(gravados);
            }

            return new List<DataDTO>();
        }

        private static List<DataDTO> TryGetList(string body)
        {
            try
            {
                return JsonSerializer.Deserialize<Dictionary<string, DataDTO>>(body).Values.ToList();
            }
            catch
            {
                return new List<DataDTO>();
            }
        }
    }
}
