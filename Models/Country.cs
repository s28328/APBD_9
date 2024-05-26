﻿using System;
using System.Collections.Generic;

namespace APBD_9.Models;

public partial class Country
{
    public int Idcountry { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Trip> Idtrips { get; set; } = new List<Trip>();
}
