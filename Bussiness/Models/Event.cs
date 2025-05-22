using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bussiness.Models;

public class Event
{
    public string Id { get; set; } = null!;

    public string? Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Location { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime EventDate { get; set; }

    public DateTime Time { get; set; }

    public string? Image { get; set; }

    public string? Category { get; set; }

    public string? Status { get; set; }
}
