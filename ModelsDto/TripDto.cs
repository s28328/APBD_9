using System.Collections;
using APBD_9.Models;

namespace APBD_9.ModelsDto;

public class TripDto
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Datefrom { get; set; }

    public DateTime Dateto { get; set; }

    public int Maxpeople { get; set; }
    public IEnumerable<ClientDto> Clients { get; set; }
    public IEnumerable<CountryDto> Countries { get; set; }
}