using APBD_9.Models;
using APBD_9.ModelsDto;

namespace APBD_9.Repositories;

public interface IClientTripRepository
{
    public Task<bool> IsClientWithTrip(int clientId);
    public Task<int> CreateClientTrip(ClientTrip clientTrip);
}