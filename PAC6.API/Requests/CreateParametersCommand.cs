namespace PAC6.API.Requests
{
    public class CreateParametersCommand
    {
        public float MaxTemperatura { get; set; }
        public float MinTemperatura { get; set; }
        public float MaxHumidade { get; set; }
        public float MinHumidade { get; set; }
        public required string API_KEY { get; set; }
    }
}
