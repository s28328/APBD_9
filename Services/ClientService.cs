using APBD_9.Contex;
using APBD_9.Exceptions;
using APBD_9.Models;
using APBD_9.Repositories;
using Microsoft.EntityFrameworkCore;

namespace APBD_9.Services;

public class ClientService:IClientService
{
    private IClientRepository _clientRepository;
    private IClientTripRepository _clientTripRepository;
    private PostgresContext _postgresContext = new PostgresContext();

    public ClientService(IClientRepository clientRepository, IClientTripRepository clientTripRepository)
    {
        _clientRepository = clientRepository;
        _clientTripRepository = clientTripRepository;
    }

    public async Task<int> RemoveClient(int clientId)
    {
        if (await ClientHasTrips(clientId))
        {
            return -1;
        }
        var result = await _clientRepository.RemoveClient(clientId);
        return result;
    }

    public async Task<int> Exists(string pesel)
    {
        if (await _clientRepository.ExistsWithPesel(pesel))
        {
            throw new DomainException("Client with provided Pesel is already exists.");
        }
        return 1;
    }

    public async Task<int> CreateClient(Client client)
    {
        var clientId = await _clientRepository.CreateClient(client);
        return clientId;
    }

    private async Task<bool> ClientHasTrips(int clientId)
    {
        return await _clientTripRepository.IsClientWithTrip(clientId);
    }
}