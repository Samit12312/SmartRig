namespace Models.ViewModels
{
    public class EditUserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserAddress { get; set; }
        public string UserPhoneNumber { get; set; }
        public int CityId { get; set; }
        public bool Manager { get; set; }
    }
}