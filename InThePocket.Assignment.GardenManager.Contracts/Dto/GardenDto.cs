namespace InThePocket.Assignment.GardenManager.Contracts.Dto
{
    public class GardenDto
    {
        public Guid GardenId { get; set; }
        public string GardenName { get; set; }
        public double TotalSurfaceArea { get; set; }
        public string LocationDescription { get; set; }
    }
}