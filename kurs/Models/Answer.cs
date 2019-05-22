using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs.Models
{
    public class Answer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Content { get; set; }

        public int Counter { get; set; }

        public int? PointId { get; set; }

        public virtual Point Point { get; set; }
    }
}
