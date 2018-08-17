using Microsoft.EntityFrameworkCore;

namespace LoginReg.Models
{
    public class LoginRegContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public LoginRegContext(DbContextOptions<LoginRegContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Pet> pets { get; set; }
        public DbSet<Like> likes { get; set; }
        public DbSet<Comment> comment { get; set; }
    }
}