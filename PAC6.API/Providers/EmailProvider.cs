using FluentEmail;
using PAC6.API.DTO;
using PAC6.API.Interfaces;
using System.Net.Mail;

namespace PAC6.API.Providers
{
    public class EmailProvider : IEmailProvider
    {
        public bool Handle(EmailContentDTO content, List<string> addresses)
        {
            try
            {
                foreach (var address in addresses)
                {

                    SmtpClient smtp = new() 
                    {
                         Port = 587,
                         Host = "smtp-mail.outlook.com",
                         EnableSsl = true                      
                    };

                    //Email from = (Email)Email.From("gustavoh1709@hotmail.com", "gustavo h borges");

                    //Email email = new(smtp);

                    //email.Message = from.Message;

                    //email.From("gustavo1709@hotmail.com");
                    //email.Body(content.Body);
                    //email.Subject(content.Subject);

                    //email.Send();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
