using System.ComponentModel.DataAnnotations;

namespace CommandsService.Dtos
{
    public class CommandCreateDto
    {
        [Required]
        public string Howto { get; set; }
        [Required]
        public string CommandLine { get; set; }
    }
}