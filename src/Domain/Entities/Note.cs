namespace Jorda.Server.Domain.Entities;


public class Note:BaseAuditableEntity
{
    /// <summary> Name of the note </summary>
    public string Name { get; set; } = null!;

    /// <summary> Note description </summary>
    public string Description { get; set; } = null!;

    /// <summary> Source of the note (if exists), it can be a link, leading to source material </summary>
    public string? Source { get; set; }

    /// <summary> Section Id that this note is assigned to</summary>
    public Guid SectionId { get; set; }

    /// <summary> Section Id that this note is assigned to</summary>
    public Section Section { get; set; } = null!;

    /// <summary> List of labels that describe the note</summary>
    public IEnumerable<Label> Labels { get; set; } = new List<Label>();
}
