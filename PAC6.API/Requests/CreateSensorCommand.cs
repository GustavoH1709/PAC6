namespace PAC6.API.Requests
{
    public class CreateSensorCommand
    {
        public float Humidity { get; set; }
        public float Temperature { get; set; }
        public required string API_KEY { get; set; }
    }
}
