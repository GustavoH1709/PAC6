using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace PAC6.API.Providers
{
    public class FirebaseConnectionProvider
    {
        public IFirebaseClient Connection;

        private readonly string FIREBASE_URL;
        private readonly string FIREBASE_KEY;

        public FirebaseConnectionProvider()
        {
            FIREBASE_URL = Environment.GetEnvironmentVariable("FIREBASE_URL") ?? string.Empty;
            FIREBASE_KEY = Environment.GetEnvironmentVariable("FIREBASE_KEY") ?? string.Empty;
            Load();
        }

        private void Load()
        {
            IFirebaseConfig config = new FirebaseConfig()
            {
                AuthSecret = FIREBASE_KEY,
                BasePath = FIREBASE_URL
            };

            IFirebaseClient client = new FirebaseClient(config);

            Connection = client ?? throw new ArgumentNullException("Could not start firebase connection", nameof(client)); ;
        }
    }
}
