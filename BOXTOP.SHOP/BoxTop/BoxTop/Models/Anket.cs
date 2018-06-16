using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoxTop.Models
{
    public class UserPreferences
    {
        public int ID { get; set; }
        public Dictionary<Category, int> UserPrefer = new Dictionary<Category, int>();
    }

    public class Anket
    {
        [Display(Name = "Ваше Имя")]
        public string Name { get; set; }

        [Display(Name = "Кому дарите (будет написано в открытке)")]
        public string InfoTarget { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Range(0, 100000000)]
        [Display(Name = "Сумма, которую рассчитываете потратить")]
        public float Money { get; set; }

        [Required]
        [Display(Name = "Адресс доставки")]
        public string Adress { get; set; }

        [Required]
        [Range(1, 10)]
        [Display(Name = "Увлечение компьютером (по 10-бальной шкале)")]
        public decimal Computer { get; set; }

        [Required]
        [Range(1, 10)]
        [Display(Name = "Увлечение книгами (по 10-бальной шкале)")]
        public decimal Book { get; set; }


    }
}
