using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emotionsAPI.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        public long idProduct { get; set; }

        public long idUser { get; set; }

        [DataType(DataType.MultilineText)]
        public String message { get; set; } = null!;

        public string emotion { get; set; } = "neutral";

        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}
