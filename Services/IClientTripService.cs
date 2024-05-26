using APBD_9.Models;
using APBD_9.ModelsRequest;

namespace APBD_9.Services;

public interface IClientTripService
{
    public Task<int> CreateClientTrip(ClientTrip addClientTrip);
}