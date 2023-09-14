using PAC6.API.Requests;

namespace PAC6.API.Interfaces
{
    public interface ICreateEmailApplication
    {
        Task<bool> Handle(CreateEmailCommand command);
    }
}
