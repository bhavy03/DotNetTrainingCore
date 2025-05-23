﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FirstWebApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public required string Name { get; set; }
        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }
        [Required]
        public required string Category { get; set; }
    }
}
