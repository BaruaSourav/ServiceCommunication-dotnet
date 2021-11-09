using System.ComponentModel.DataAnnotations;

namespace PlatformService.Dtos
{
    public class PlatformCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Publisher {get; set;}
        [Required]
        public decimal Cost { get; set; }
        public string InstalledBy{get; set;}
    }
}