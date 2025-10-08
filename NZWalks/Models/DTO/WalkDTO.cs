namespace NZWalks.Models.DTO
{
    public class WalkDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double LengthInKm { get; set; }
        public string? Description { get; set; }

        // Foreign Keys
        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }
    }
}
