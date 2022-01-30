using System;
using System.Threading.Tasks;

namespace MusicRoom.API.Interfaces
{
    public interface IAuthAPI
    {
        public Task<bool> Authorize();
    }
}
