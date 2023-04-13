using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Entity;
using UserManagementAPI.Models.Authentication.SignUp;

namespace UserManagementAPI.Data
{
    public class APIContext : IdentityDbContext<AddUserDetails>
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //}

        //public DbSet<RegisterUser> registerUsers { get; set; }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    SeedRoles(builder);
        //}
        //private void SeedRoles(ModelBuilder builder)
        //{
        //    builder.Entity<IdentityRole>().HasData
        //    (
        //    new IdentityRole()
        //    {
        //        Name = "Admin",
        //        ConcurrencyStamp = "1",
        //        NormalizedName = "Admin"
        //    },
        //    new IdentityRole()
        //    {
        //        Name = "User",
        //        ConcurrencyStamp = "2",
        //        NormalizedName = "User"
        //    }
        //    );
        }
    }

  

