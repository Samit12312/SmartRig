using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class EditUserViewModel // bypass user password validation when editing user info, and also bypass email validation if the email is not changed
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserAddress { get; set; }
        public string UserPhoneNumber { get; set; }
        public int CityId { get; set; }
        public bool Manager { get; set; }
    }
}
