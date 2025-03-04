﻿namespace CafeManagement.Models.Stock
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? PictureURL {  get; set; }
        public string MeasurementUnit { get; set; }
    }
}
