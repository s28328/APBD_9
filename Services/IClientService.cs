using APBD_9.Models;

namespace APBD_9.Services;

public interface IClientService
{
    public Task<int> RemoveClient(int clientId);
    public Task<int> Exists(string pesel);
    public Task<int> CreateClient(Client client);
}