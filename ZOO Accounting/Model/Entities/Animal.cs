using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ZOO_Accounting.Model.Entities
{
    [Table("Animal")]
    public class Animal:ICloneable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(10)]
        [Index(IsUnique=true)]
        public string RegistryCode { get; set; }
        public int AnimalKindId { get; set; }
        [ForeignKey("AnimalKindId")]
        public virtual AnimalKind AnimalKind { get; set; }
        public DateTime BirthDate { get; set; }
        public Sex Sex { get; set; }
        public int RationId { get; set; }
        [ForeignKey("RationId")]
        public virtual Ration Ration { get; set; }
        public int? AviaryId { get; set; }
        public virtual Aviary Aviary { get; set; }


        public Animal() { }
        public Animal(string name, string registryCode, int animalKindId, DateTime birthDate, Sex sex, int rationId){
            Name = name;
            RegistryCode = registryCode;
            BirthDate = birthDate;
            AnimalKindId = animalKindId;
            Sex = sex;
            RationId = rationId;
        }
        public override string ToString()
        {
            return string.Format("{0} - {1}", Name, RegistryCode);
        }

        public object Clone()
        {
            var copy = (Animal)MemberwiseClone();
            copy.Aviary = Aviary;
            copy.AnimalKind = AnimalKind;
            return copy;
        }
    }


    [TypeConverter(typeof(ZOO_Accounting.Helpers.EnumDescriptionTypeConverter))]
    public enum Sex
    {
        [Description("Самка")]
        Female,
        [Description("Самец")]
        Male
    }
}
