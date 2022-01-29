using System.Threading.Tasks;

namespace MusicRoom.API.Interfaces
{
    public interface IAPIFactory
    {
        Task<IPlayerAPI> BuildPlayerAPIAsync();
    }
}
