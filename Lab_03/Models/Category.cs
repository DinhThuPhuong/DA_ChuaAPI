﻿using System.ComponentModel.DataAnnotations;

namespace Lab_03.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        public List<Product>? Products1 { get; set; }

    }
}
