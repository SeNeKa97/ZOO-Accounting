using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ZOO_Accounting.Model.Entities
{
    [Table("Ration")]
    public class Ration:ICloneable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Index(IsUnique=true)]
        public string Name { get; set; }
        public string Ingredients { get; set; }

        public Ration() { }
        public Ration(string name, string ingredients) {
            Name = name;
            Ingredients = ingredients;
        }
        public override string ToString()
        {
            return this.Name;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
