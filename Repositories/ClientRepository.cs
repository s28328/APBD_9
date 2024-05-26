using APBD_9.Contex;
using APBD_9.Models;
using APBD_9.ModelsRequest;
using Microsoft.EntityFrameworkCore;

namespace APBD_9.Repositories;

public class ClientRepository:IClientRepository
{
    private readonly PostgresContext _postgresContext;

    public ClientRepository(PostgresContext postgresContext)
    {
        _postgresContext = postgresContext;
    }

    public async Task<int> RemoveClient(int clientId)
    {
        var clientToRemove = new Client()
        {
            Idclient = clientId

        };
        _postgresContext.Clients.Attach(clientToRemove);
        var entry = _postgresContext.Entry(clientToRemove);
        entry.State = EntityState.Deleted;
        await _postgresContext.SaveChangesAsync();
        return 1;
    }

    public async Task<bool> ExistsWithPesel(string pesel)
    {
        return await _postgresContext.Clients.AnyAsync(client => client.Pesel == pesel);
    }

    public async Task<int> CreateClient(Client addClient)
    {
        await _postgresContext.Clients.AddAsync(addClient);
        await _postgresContext.SaveChangesAsync();
        return addClient.Idclient;
    }
}