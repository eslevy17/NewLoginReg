using System.ComponentModel.DataAnnotations.Schema;
namespace LoginReg.Models
{
    public class Like
    {
        public int id { get; set; }

        [ForeignKey("pet")]
        public int pet_id { get; set; }
        public Pet pet { get; set; }

        [ForeignKey("user")]
        public int user_id { get; set; }
        public User user { get; set; }
    }
}