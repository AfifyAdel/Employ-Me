using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace EmployMe.Models
{
    public class ApplyForJob
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime ApllyDate { get; set; }
        public int JobId { get; set; }
        public string UserId { get; set; }

        public virtual Job Job { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}