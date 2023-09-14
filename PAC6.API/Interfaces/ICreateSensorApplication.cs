using PAC6.API.Requests;

namespace PAC6.API.Interfaces
{
    public interface ICreateSensorApplication
    {
        Task<bool> Handle(CreateSensorCommand command);
    }
}
