using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSignUp.Models
{
    public class Users
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Profile_Image { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
