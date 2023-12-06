namespace PAC6.API.Requests
{
    public class CreateEmailCommand
    {
        public required string Email { get; set; }
        public required string API_KEY { get; set; }
    }
}
