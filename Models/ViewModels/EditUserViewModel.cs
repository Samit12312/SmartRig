using System.Collections.Generic;

namespace Models.ViewModels
{
    public class EditUserViewModel
    {
        public User user { get; set; }
        public List<Cities> cities { get; set; }

        public EditUserViewModel()
        {
            user = new User();
            cities = new List<Cities>();
        }
    }
}