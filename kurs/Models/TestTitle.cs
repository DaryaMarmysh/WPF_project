using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

using System.IO;
using System.Linq;
using System.Text;

namespace kurs.Models
{
    public class TestTitle
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Point> Points { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<User> DoUsers { get; set; }
        public TestTitle()
        {
            Points = new List<Point>();
        }
    }
}