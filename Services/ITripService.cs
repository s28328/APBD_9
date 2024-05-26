using APBD_9.Models;
using APBD_9.ResponseModels;

namespace APBD_9.Services;

public interface ITripService
{
    public Task<PageTripsModel> GetTrips(int pageNum, int pageSize);
    public Task<Trip> Exists(int tripId);
    public int DateFromIsValid(DateTime dateFrom);
}