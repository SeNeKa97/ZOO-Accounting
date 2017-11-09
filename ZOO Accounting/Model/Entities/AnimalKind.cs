using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using ZOO_Accounting.Helpers;

namespace ZOO_Accounting.Model.Entities
{
    [Table("AnimalKind")]
    public class AnimalKind:ICloneable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Index(IsUnique=true)]
        public string Name { get; set; }
        public Biome Biome { get; set; }
        public EatingStrategy EatingStrategy { get; set; }
        public override string ToString() {
            return this.Name;
        }


        public AnimalKind() { }
        public AnimalKind(string name, Biome biome, EatingStrategy eatingStrategy) {
            Name = name;
            Biome = biome;
            EatingStrategy = eatingStrategy;
        }
        public object Clone()
        {
            var copy = (AnimalKind)MemberwiseClone();
            return copy;
        }
    }



    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Biome
    {
        [Description("Полярные пустыни")]
        PolarDesert,

        [Description("Тундра")]
        Tundra,

        [Description("Тайга")]
        Taiga,

        [Description(" Смеш. и листв. леса")]
        MixedForests,

        [Description("Степи и лесостепи")]
        Steppe,

        [Description("Субтропические дождевые леса")]
        SubTropicForrest,

        [Description("Средиземноморские биомы")]
        MediTerranBiomes,

        [Description("Муссонные леса")]
        MussonForests,

        [Description("Аридные пустыни ")]
        AridDeserts,

        [Description("Ксерофитные кустарники")]
        Xenofites,

        [Description("Южные степи")]
        SouthernSteppe,

        [Description("Семиаридные пустыни")]
        SemiaridDeserts,

        [Description("Саванны")]
        Savannah,

        [Description("Саванны-лесостепи")]
        SavannahForestSteppe,

        [Description("Субтропический лес")]
        SubtropicForest,

        [Description("Тропический дождевой лес")]
        TropicRainForest,

        [Description("Альпийская тундра")]
        AlpenTundra,

        [Description("Горные леса")]
        MountainForests
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum EatingStrategy
    {
        [Description("Травоядный")]
        Herbivore,

        [Description("Плотоядный")]
        Carnivore,

        [Description("Всеядный")]
        Omnivore
    }
}
