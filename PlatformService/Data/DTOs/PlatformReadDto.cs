namespace PlatformService.Dtos
{
    public class PlatformReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Publisher {get; set;}
        public decimal Cost { get; set; }
        public string InstalledBy{get; set;}
    }
}