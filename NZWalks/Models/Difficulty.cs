using System;
using System.Collections.Generic;

namespace NZWalks.Models;

public partial class Difficulty
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Walk> Walks { get; set; } = new List<Walk>();
}
