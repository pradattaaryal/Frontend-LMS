using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class Author
    {
        public int AuthorID { get; set; }

        public string Name { get; set; }

        public string Bio { get; set; }
    }
}
