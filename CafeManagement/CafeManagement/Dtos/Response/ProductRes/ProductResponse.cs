﻿namespace CafeManagement.Dtos.Response.ProductRes
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Img { get; set; }
        public string? Description { get; set; }
        public string CategoryName { get; set; }
    }
}
