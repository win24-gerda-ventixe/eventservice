using System.ComponentModel.DataAnnotations;

namespace Presentation.Data;

public class EventEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; } = null!;

}
