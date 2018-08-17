using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LoginReg.Models
{
    public class Comment
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Leave a message!")]
        [MaxLength(ErrorMessage = "Message must be 256 characters or less.")]
        public string content { get; set; }

        [ForeignKey("pet")]
        public int pet_id { get; set; }
        public Pet pet { get; set; }

        [ForeignKey("user")]
        public int user_id { get; set; }
        public User user { get; set; }

        public DateTime created_at { get; set; }

    }
}