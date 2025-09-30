using Models;
namespace Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ModelValidation();
        }
        static void ModelValidation()
        {
            User u1 = new User();
            u1.UserName = "12";
            u1.UserEmail = "123";
            u1.UserPassword = "sda";
            u1.UserAddress = "";
            u1.UserPhoneNumber = "123";
            Dictionary<string, List<String>> errors = u1.AllErrors();
            if (u1.IsValid == false)
            {
                foreach (var error in errors)
                {
                    foreach (var errorsmsg in error.Value)
                    {
                        Console.WriteLine(errorsmsg);
                    }
                }
            }
        }
    }
}
