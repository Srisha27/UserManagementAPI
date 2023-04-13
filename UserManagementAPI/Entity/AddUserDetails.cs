using Microsoft.AspNetCore.Identity;

namespace UserManagementAPI.Entity
{
    public class AddUserDetails : IdentityUser
    {
        public string? FirstName { get; set; }    

        public string? LastName { get; set; }

        public DateTime DOB { get; set; }

        public string ? Address { get; set;}

    }
}
