using APBD_9.Contex;
using APBD_9.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace APBD_9.Repositories;

public class TripRepository:ITripRepository
{
    private PostgresContext _postgresContext;

    public TripRepository(PostgresContext postgresContext)
    {
        _postgresContext = postgresContext;
    }

    public async Task<IEnumerable<Trip>> GetTrips(int pageNum, int pageSize)
    {
        var trips = await _postgresContext.Trips
            .Include(trip => trip.Countries)
            .Include(trip => trip.ClientTrips)
            .ThenInclude(trip => trip.IdclientNavigation)
            .OrderByDescending(trip => trip.Datefrom)
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return trips;
    }

    public async Task<int> GetCountTrips()
    {
        var count = await _postgresContext.Trips.CountAsync();
        return count;
    }

    public async Task<Trip?> GetTrip(int tripId)
    {
        var trip = await _postgresContext.Trips.SingleOrDefaultAsync(t => t.Idtrip == tripId);
        return trip;
    }
}