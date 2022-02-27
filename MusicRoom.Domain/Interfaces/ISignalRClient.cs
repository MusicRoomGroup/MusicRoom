using System.Threading.Tasks;

namespace MusicRoom.SignalRClient.Interfaces
{
    public interface ISignalRClient
    {
	    Task ReceiveMessage(string user, string message);
    }
}
