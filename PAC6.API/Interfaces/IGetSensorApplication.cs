using PAC6.API.DTO;

namespace PAC6.API.Interfaces
{
    public interface IGetSensorApplication
    {
        Task<List<DataDTO>> Handle();
    }
}
