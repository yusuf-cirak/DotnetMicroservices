using System.ComponentModel.DataAnnotations;

namespace CommandsService.Dtos
{
    public sealed class CommandCreateDto
    {
        [Required]
        public string HowTo { get; set; }

        [Required]
        public string CommandLine { get; set; }
    }
}