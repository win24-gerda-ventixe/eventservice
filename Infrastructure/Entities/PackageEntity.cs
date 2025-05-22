using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities;

public class PackageEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? SeatingArrangment { get; set; }

    public string? Placement { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Price { get; set; }

    public string? Currency { get; set; }

}
