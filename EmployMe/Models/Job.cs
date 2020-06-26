using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployMe.Models
{
    public class Job
    {
        public int Id { get; set; }
        [DisplayName("Job Name")]
        public string JobTitle { get; set; }
        [DisplayName("Job Content")]
        public string JobContent { get; set; }
        [DisplayName("Job Image")]
        public string JobImage { get; set; }
        [DisplayName("Job Type")]
        public int CategoryId { get; set; }
        public virtual Category category { get; set; } // virtual for lazy loading
    }
}