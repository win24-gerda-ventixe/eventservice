using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

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

