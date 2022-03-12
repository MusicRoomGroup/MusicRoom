using System;
using System.Threading.Tasks;

namespace MusicRoom.API.Interfaces
{
    public interface IAuthApi
    {
        public Task<bool> Authorize();
    }
}
