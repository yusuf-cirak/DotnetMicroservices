using System.ComponentModel.DataAnnotations;

namespace CommandsService.Models;

public sealed class Platform
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int ExternalID { get; set; }
    [Required]

    public string Name { get; set; }
    public ICollection<Command> Commands { get; set; } = new HashSet<Command>();
}


