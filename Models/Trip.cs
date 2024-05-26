using System;
using System.Collections.Generic;

namespace APBD_9.Models;

public partial class Trip
{
    public int Idtrip { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Datefrom { get; set; }

    public DateTime Dateto { get; set; }

    public int Maxpeople { get; set; }

    public virtual ICollection<ClientTrip> ClientTrips { get; set; } = new List<ClientTrip>();

    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}
