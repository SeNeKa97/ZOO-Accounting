using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ZOO_Accounting.Model.Entities
{
    [Table("InventoryType")]
    public class InventoryType:ICloneable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        [Index(IsUnique=true)]
        public string Name { get; set; }

        public InventoryType(){ }
        public InventoryType(string name) { this.Name = name; }
        public override string ToString() {
            return Name;
        }
        public object Clone() {
            return (InventoryType)MemberwiseClone();
        }
    }
}
