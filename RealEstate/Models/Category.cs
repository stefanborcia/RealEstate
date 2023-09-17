﻿using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Category
    {
        [Required]  
        public int Id { get; set; }
        [Required(ErrorMessage = "Category name can't be null or empty")]  // validation
        public string Name { get; set; }
        [Required(ErrorMessage = "Image url can't be null or empty")]
        public string ImageUrl { get; set; }
        public ICollection<Property>Properties { get; set; } // 1-Many relationship between Category and Property
    }
}
