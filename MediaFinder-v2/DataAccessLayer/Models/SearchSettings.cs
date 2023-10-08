﻿namespace MediaFinder_v2.DataAccessLayer.Models;

public record SearchSettings
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<SearchDirectory> Directories { get; set; } = null!;

    public bool Recursive { get; set; }

    public bool ExtractArchives { get; set; }

    public int? ExtractionDepth { get; set; }

    public bool PerformDeepAnalysis { get; set; }
}
