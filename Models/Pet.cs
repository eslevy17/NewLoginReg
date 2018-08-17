using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace LoginReg.Models
{
    public class Pet
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MinLength(2, ErrorMessage = "Name must be between 2 and 45 letters.")]
        [MaxLength(2, ErrorMessage = "Name must be between 2 and 45 letters.")]
        public string name { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        [MaxLength(256, ErrorMessage="Image URL must be 256 characters or less.")]
        public string image { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(256, ErrorMessage="Description must be 256 characters or less.")]
        public string description { get; set; }

        public DateTime created_at { get; set; }

        [ForeignKey("user")]
        public int posted_by { get; set; }
        public User user { get; set; }

        public List<Comment> comments { get; set; }
        public List<Like> likes { get; set; }

        public Pet()
        {
            comments = new List<Comment>();
            likes = new List<Like>();
        }
    }
}