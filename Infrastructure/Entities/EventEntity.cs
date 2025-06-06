using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;
// There is a logical mistake, i declare some fields as required,
// but in the constructor i set them to null.
// I am aware it needs to be fixed but since it's my entitiy, i was unsure whether
// applying new migrations and updating the database would affect the existing data.

// I would also probably consider creating a separate microservice for event packages
// and let events focus on the event details only.


public class EventEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string? Title { get; set; } = null!;

    public string? Description { get; set; }

    [Required]
    public string? Location { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    public DateTime EventDate { get; set; }

    public DateTime Time { get; set; }

    public string? Image { get; set; }

    public string? Category { get; set; }

    public string? Status { get; set; }

    public ICollection<EventPackageEntity> Packages { get; set; } = [];
}

