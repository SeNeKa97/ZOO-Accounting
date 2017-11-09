using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ZOO_Accounting.Model.Entities
{
    [Table("Inventory")]
    public class Inventory:ICloneable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        [Index(IsUnique=true)]
        public string RegistryCode { get; set; }
        public int InventoryTypeId { get; set; }
        [ForeignKey("InventoryTypeId")]
        public virtual InventoryType InventoryType { get; set; }
        public int? AviaryId { get; set; }
        [ForeignKey("AviaryId")]
        public virtual Aviary Aviary { get; set; }

        public Inventory(){}
        public Inventory(string regCode, int inventoryTypeId)
        {
            RegistryCode = regCode;
            InventoryTypeId = inventoryTypeId;
        }
        public override string ToString()
        {
            return RegistryCode;
        }

        public object Clone()
        {
            var copy = (Inventory)MemberwiseClone();

            copy.InventoryType = InventoryType;
            copy.Aviary = Aviary;
            return copy;
        }
    }
}
