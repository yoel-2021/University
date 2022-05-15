using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UniversityApiBackend.Models.DataModels
{
    public class User:BaseEntity
    {

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [Required,StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required,EmailAddress]
        public string Email { get; set; } = string.Empty ;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public bool IsAdministrator { get; set; } = false;
    }
}


