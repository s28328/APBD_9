using APBD_9.Contex;
using APBD_9.Models;
using APBD_9.ModelsRequest;
using APBD_9.Repositories;

namespace APBD_9.Services;

public class ClientTripService:IClientTripService
{
    private readonly IClientTripRepository _clientTripRepository;

    public ClientTripService(IClientTripRepository clientTripRepository)
    {
        _clientTripRepository = clientTripRepository;
    }

    public async Task<int> CreateClientTrip(ClientTrip addClientTrip)
    {
        await _clientTripRepository.CreateClientTrip(addClientTrip);
        return 1;
    }
}