using APBD_9.Exceptions;
using APBD_9.Models;
using APBD_9.ModelsDto;
using APBD_9.Repositories;
using APBD_9.ResponseModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APBD_9.Services;

public class TripService:ITripService
{
    private ITripRepository _tripRepository;

    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    private void isValidQueryParams(int pageNum, int pageSize)
    {
        if (pageNum <= 0 || pageSize <= 0)
            throw new DomainException("Invalid query parameters");
    }

    public async Task<PageTripsModel> GetTrips(int pageNum, int pageSize)
    {
        isValidQueryParams(pageNum,pageSize);
        var trips = await _tripRepository.GetTrips(pageNum, pageSize);
        var tripsClientsCountries = trips.Select(trip => new TripDto()
        {
            Name = trip.Name,
            Description = trip.Description,
            Datefrom = trip.Datefrom,
            Dateto = trip.Dateto,
            Maxpeople = trip.Maxpeople,
            Clients = trip.ClientTrips.Select(clientTrip => new ClientDto()
            {
                FirstName = clientTrip.IdclientNavigation.Firstname,
                LastName = clientTrip.IdclientNavigation.Lastname
            }).ToList(),
            Countries = trip.Countries.Select(country => new CountryDto()
            {
                Name = country.Name
            }).ToList()
        }).ToList();
        var count = await _tripRepository.GetCountTrips();
        var allPages = count / pageSize;
        if (count % pageSize != 0)
        {
            allPages++;
        }
        var pageTripsModel = new PageTripsModel()
        {
            PageNum = pageNum,
            AllPages = allPages,
            PageSize = pageSize,
            Trips = tripsClientsCountries
        };
        return pageTripsModel;
    }

    public async Task<Trip> Exists(int tripId)
    {
        var trip = await _tripRepository.GetTrip(tripId);
        if ( trip == null)
        {
            throw new DomainException("No trip with provided Id.");
        }
        return trip;
    }

    public int DateFromIsValid(DateTime dateFrom)
    {
        if (dateFrom < DateTime.Today)
        {
            throw new DomainException("Trip is expired.");
        }

        return 1;
    }
}