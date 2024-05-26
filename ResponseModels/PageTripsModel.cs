using APBD_9.Models;
using APBD_9.ModelsDto;

namespace APBD_9.ResponseModels;

public class PageTripsModel
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int AllPages { get; set; }
    public IEnumerable<TripDto>? Trips { get; set; }
}