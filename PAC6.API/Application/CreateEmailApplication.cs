using FluentEmail;
using PAC6.API.DTO;
using PAC6.API.Interfaces;
using PAC6.API.Providers;
using PAC6.API.Requests;
using PAC6.API.Validators;
using System.Net;
using System.Text.RegularExpressions;

namespace PAC6.API.Application
{
    public class CreateEmailApplication : ICreateEmailApplication
    {
        private readonly FirebaseConnectionProvider _firebase;
        private readonly string _regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        public CreateEmailApplication()
        {
            _firebase = new();
        }

        public async Task<bool> Handle(CreateEmailCommand command)
        {
            if (!KeyValidator.IsValid(command.API_KEY))
            {
                return false;
            }

            if (!Regex.IsMatch(command.Email, _regex, RegexOptions.IgnoreCase))
            {
                return false;
            }

            command.Email = (command.Email ?? "").Trim();

            var response = _firebase.Connection.Push("Emails/", new EmailDTO()
            {
                Email = command.Email,
                CreatedAt = DateTime.Now
            });

            return await Task.FromResult(response != null && response.StatusCode == HttpStatusCode.OK);
        }
    }
}
