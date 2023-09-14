using PAC6.API.DTO;

namespace PAC6.API.Interfaces
{
    public interface IEmailProvider
    {
        bool Handle(EmailContentDTO content, List<string> addresses);
    }
}
