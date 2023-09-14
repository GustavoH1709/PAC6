using PAC6.API.Requests;

namespace PAC6.API.Interfaces
{
    public interface ICreateParametersApplication
    {
        Task<bool> Handle(CreateParametersCommand command);
    }
}
