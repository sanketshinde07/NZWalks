using System;
using System.Collections.Generic;

namespace NZWalks.Models;

public partial class Region
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? RegionImageUrl { get; set; }

    public virtual ICollection<Walk> Walks { get; set; } = new List<Walk>();
}
