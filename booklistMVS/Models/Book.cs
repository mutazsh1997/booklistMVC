using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace booklistMVS.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string AuthName{ get; set; }
        [Required]
        public string BookName{ get; set; }
        public DateTime BookYear{ get; set; }
    }
}
