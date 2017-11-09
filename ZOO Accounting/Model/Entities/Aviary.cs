using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ZOO_Accounting.Model.Entities
{
    [Table("Aviary")]
    public class Aviary:ICloneable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [Index(IsUnique=true)]
        public string Code { get; set; }
        public int Capacity { get; set; }
        public int AviaryTypeId { get; set; }
        [ForeignKey("AviaryTypeId")]
        public virtual AviaryType AviaryType { get; set; }
        public virtual ICollection<Animal> Animals { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }

        public Aviary()
        {
            Animals = new HashSet<Animal>();
            Inventories = new HashSet<Inventory>();
        }
        public Aviary(string code, int capacity, int aviaryTypeId, ICollection<Animal> animals, ICollection<Inventory> inventories)
        {
            Code = code;
            Capacity = capacity;
            AviaryTypeId = aviaryTypeId;
            Animals = animals;
            Inventories = inventories;
        }
        public override string ToString()
        {
            return Code;
        }
        public object Clone()
        {
            var copy = (Aviary)MemberwiseClone();

            copy.Animals = Animals;
            copy.Inventories = Inventories;
            copy.AviaryType = AviaryType;

            return copy;
        }
    }
}
