using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CookBookModel
{
    [Table("Recipes", Schema = "Hierarchy")]
    [Index("Title", IsUnique = true)]
    public class Recipe
    {
        public Guid Id { get; set; }

        [Required]
        public HierarchyId Level { get; set; }

        [Required]
        [MaxLength(256)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
    }
}