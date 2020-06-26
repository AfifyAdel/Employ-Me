using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployMe.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Job Type")]
        public string CategoryName { get; set; }
        [Display(Name = "Job Description")]
        public string CategoryDescription { get; set; }
        public  virtual ICollection<Job> Jobs { get; set; } // virtual for lazy loading
    }
}