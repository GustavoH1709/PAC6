using PAC6.API.DTO;
using PAC6.API.Interfaces;
using PAC6.API.Providers;

namespace PAC6.API.Application
{
    public class TesteHttpApplication : ITesteHttpApplication
    {
        private readonly FirebaseConnectionProvider _firebase;
        private string log;

        public TesteHttpApplication()
        {
            _firebase = new();
            log = "";
        }

        public async Task<string> Handle()
        {
            await TryHttpClient();
            await TryFirebase();
            return log;
        }

        private async Task TryHttpClient()
        {
            try
            {
                using HttpClient _httpClient = new();
                await _httpClient.PostAsync("https://youtube.com", null);
                log += " HTTP CLIENT - deu boa ";
            }
            catch (Exception ex)
            {
                log += " HTTP CLIENT - " + ex.Message + " //////////////////////////////////////////////////// ";
            }
        }

        private async Task TryFirebase()
        {
            try
            {
                _firebase.Connection.Get("Emails/");
                log += " FIREBASE - deu boa ";
            }
            catch (Exception ex)
            {            
                log += " FIREBASE - " + ex.Message + " //////////////////////////////////////////////////// ";
            }
        }
    }
}
