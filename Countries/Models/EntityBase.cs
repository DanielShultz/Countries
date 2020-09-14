using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Countries.Models
{
    public class EntityBase
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }
    }
}