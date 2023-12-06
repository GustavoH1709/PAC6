namespace PAC6.API.Validators
{
    public static class KeyValidator
    {
        public static bool IsValid(string key)
        {
            return (key ?? "").Equals("pac6");
        }
    }
}
