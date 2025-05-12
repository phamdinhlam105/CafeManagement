using CafeManagement.Interfaces;

namespace CafeManagement.Models.Stock
{
    public class Ingredient:ISoftDeletable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? PictureURL {  get; set; }
        public string MeasurementUnit { get; set; }
        public bool IsDeleted {  get; set; }
        public Ingredient()
        {
            IsDeleted = false;
        }
    }
}
