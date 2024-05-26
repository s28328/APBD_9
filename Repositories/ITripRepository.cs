using APBD_9.Models;

namespace APBD_9.Repositories;

public interface ITripRepository
{
    public Task<IEnumerable<Trip>> GetTrips(int pageNum, int pageSize);
    public Task<int> GetCountTrips();
    public Task<Trip?> GetTrip(int tripId);
}