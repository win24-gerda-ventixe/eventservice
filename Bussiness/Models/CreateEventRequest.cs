
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bussiness.Models;

public class CreateEventRequest
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(200)]
    public string Location { get; set; } = null!;

    //[Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    public DateTime EventDate { get; set; }

    [Required]
    public DateTime Time { get; set; }

    //[MaxLength(2048)]
    public string? Image { get; set; }

    [MaxLength(50)]
    public string? Category { get; set; }

    [MaxLength(20)]
    public string? Status { get; set; }
}

