namespace InThePocket.Assignment.GardenManager.Contracts.Api.Dto
{
    public class GardenDto
    {
        public Guid GardenId { get; set; }
        public string GardenName { get; set; }
        public double TotalSurfaceArea { get; set; }
        public string LocationDescription { get; set; }
        public int TargetHumidityLevel { get; set; }
        public Guid UserId { get; set; }
    }
}