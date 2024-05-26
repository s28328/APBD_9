using APBD_9.Models;
using APBD_9.ModelsRequest;

namespace APBD_9.Repositories;

public interface IClientRepository
{
    public Task<int> RemoveClient(int clientId);
    public Task<bool> ExistsWithPesel(string pesel);
    public Task<int> CreateClient(Client addClient);

}