using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs.Models
{
    public class Point
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public int? TestTitleId { get; set; }
        public TestTitle TestTitle { get; set; }
        
        public Point()
        {
            Answers = new List<Answer>();
        }
    }
}
