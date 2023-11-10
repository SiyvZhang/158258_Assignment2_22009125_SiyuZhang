using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Assignment2.Models
{
    public class Food
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "The Food name cannot be blank")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter a Food name between 3 and 50 characters in length")] 
        [RegularExpression(@"^[a-zA-Z0-9,\s]*$", ErrorMessage = "Please enter a Food name made up of only letters, numbers and spaces")]
        [Display(Name = "Food name")]
        public String Name { get; set; }

        [Required(ErrorMessage = "The Food description cannot be blank")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Please enter a Food description between 10 and 200 characters in length")] 
        [RegularExpression(@"^[a-zA-Z0-9,\s]*$", ErrorMessage = "Please enter a Food description made up of only letters, numbers and spaces")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Food price cannot be blank")]
        [Range(0.10, 10000, ErrorMessage = "Please enter a Food price between 0.10 and 10000")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }

        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}