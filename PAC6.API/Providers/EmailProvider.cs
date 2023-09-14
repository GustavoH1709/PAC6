using FluentEmail;
using PAC6.API.DTO;
using PAC6.API.Interfaces;

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
                    Email.FromDefault()
                         .To(address)
                         .Subject(content.Subject)
                         .Body(content.Body)
                         .Send();
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
