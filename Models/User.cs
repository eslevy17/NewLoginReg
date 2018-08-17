using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace LoginReg.Models
{
    public class ViewUser
    {
        public int id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters.")]
        [RegularExpression(@"^[a-zA-Z""'\s-]*$", ErrorMessage = "First name can only contain letters.")]
        public string first_name { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters.")]
        [RegularExpression(@"^[a-zA-Z""'\s-]*$", ErrorMessage = "Last name can only contain letters.")]
        public string last_name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Must be a valid email.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        public string password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required.")]
        [Compare("password", ErrorMessage = "Passwords must match.")]
        public string confirm_password { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public List<Comment> comments { get; set; }
        public List<Like> likes { get; set; }
        public List<Pet> pets { get; set; }

        public User()
        {
            comments = new List<Comment>();
            likes = new List<Like>();
            pets = new List<Pet>();
        }
    }
}