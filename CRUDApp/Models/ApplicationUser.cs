using Microsoft.AspNetCore.Identity;

namespace CRUDApp.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string FName { get; set; }
        public string LName { get; set; }   
    }
}
