﻿using System.ComponentModel.DataAnnotations;

namespace Lab2.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
    }
}
