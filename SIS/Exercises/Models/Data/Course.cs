using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercises.Models.Data
{
    public class Course
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; }
    }
}