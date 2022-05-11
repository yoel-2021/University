using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace UniversityApiBackend.Models.DataModels
{
    public enum Level
    {
        Basic,
        Medium,
        Advanced,
        Expert

    }
    public class Course: BaseEntity
    {
        [Required,StringLength(100)]
        public string name { get; set; } = string.Empty;
        
        [Required,StringLength(200)]
        public string Shortdescription { get; set; } = string.Empty;

        [Required]
        public string description { get; set; } = string.Empty;

        public Level Level { get; set; } = Level.Basic;

        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        [Required]
        public Chapter Chapter { get; set; } = new Chapter();

        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();


    }
}
