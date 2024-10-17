﻿namespace CafeManagement.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public ICollection<Product> Products { get; set; }

        public Category()
        {
            Products = new List<Product>(); 
        }
    }
}
