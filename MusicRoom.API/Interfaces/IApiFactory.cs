using System.Threading.Tasks;

namespace MusicRoom.API.Interfaces
{
    public interface IApiFactory
    {
        Task<IPlayerApi> BuildPlayerAPIAsync();
    }
}
