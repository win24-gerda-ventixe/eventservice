
namespace Bussiness.Models;

public class CreateEventRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public decimal Price { get; set; }      
    public DateTime EventDate { get; set; }  
    public DateTime Time { get; set; }
    public string? Image { get; set; }       
    public string? Category { get; set; }    
    public string? Status { get; set; }     
}

