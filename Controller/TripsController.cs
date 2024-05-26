using APBD_9.Exceptions;
using APBD_9.Models;
using APBD_9.ModelsRequest;
using APBD_9.Repositories;
using APBD_9.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_9;
[Route("api/[controller]")]
[ApiController]
public class TripsController:ControllerBase
{
    private readonly ITripService _tripService;
    private readonly IClientService _clientService;
    private readonly IClientTripService _clientTripService;

    public TripsController(ITripService tripService, IClientRepository clientRepository, IClientService clientService, IClientTripService clientTripService)
    {
        _tripService = tripService;
        _clientService = clientService;
        _clientTripService = clientTripService;
    }
    [HttpGet]
    public async Task<IActionResult> GetTrpis([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var pageTrips = await _tripService.GetTrips(page, pageSize);
        return Ok(pageTrips);
    }
    [Route("api/clients/{idClient:int}")]
    [HttpDelete]
    public async Task<IActionResult> RemoveClient(int idClient)
    {
        var effectedCount = await _clientService.RemoveClient(idClient);
        if (effectedCount == -1)
        {
            return StatusCode(StatusCodes.Status304NotModified);
        }

        return StatusCode(StatusCodes.Status200OK);
    }
    [Route("/api/trips/clients")]
    [HttpPost]
    public async Task<IActionResult> CreateClientTrip(AddClientTrip addClientTrip)
    {
        await _clientService.Exists(addClientTrip.Pesel);
        var trip = await _tripService.Exists(addClientTrip.IdTrip);
        _tripService.DateFromIsValid(trip.Datefrom);
        var clientId = await _clientService.CreateClient(new Client()
        {
            Firstname = addClientTrip.FirstName,
            Lastname = addClientTrip.LastName,
            Email = addClientTrip.Email,
            Pesel = addClientTrip.Pesel,
            Telephone = addClientTrip.Telephone
        });
        await _clientTripService.CreateClientTrip(new ClientTrip()
        {
            Idclient = clientId,
            Idtrip = trip.Idtrip,
            Paymentdate = addClientTrip.PaymentDate,
            Registeredat = DateTime.Today
        });
        return Ok();
    }
}