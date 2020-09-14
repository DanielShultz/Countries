using System.ComponentModel.DataAnnotations;

namespace Countries.Models
{
    public class Country : EntityBase
    {
        [Display(Name = "Код страны")]
        public string CountryCode { get; set; }
        [Display(Name = "Столица")]
        public virtual City Capital { get; set; }
        [Display(Name = "Площадь")]
        public double? Area { get; set; }
        [Display(Name = "Население")]
        public int Population { get; set; }
        [Display(Name = "Регион")]
        public virtual Region Region { get; set; }
    }
}