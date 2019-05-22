using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs.Models
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<TestTitle> TestTitles { get; set; }

        public Category()
        {
            TestTitles = new List<TestTitle>();
        }
    }
    
}