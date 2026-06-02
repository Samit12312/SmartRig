using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class CheckoutViewModel : Model
    {
        private string cardHolderName;
        private string cardNumber;
        private string cardDate;
        private string cvv;
        private string address;

        [Required(ErrorMessage = "You must enter card holder name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Card holder name must be between 2 and 40 characters")]
        [RegularExpression(@"^(?=.*[A-Za-zא-ת])[A-Za-zא-ת ]+$", ErrorMessage = "Card holder name can contain only letters and spaces")]
        public string CardHolderName
        {
            get { return cardHolderName; }
            set
            {
                cardHolderName = value;
                ValidateProperty(value, "CardHolderName");
            }
        }

        [Required(ErrorMessage = "You must enter card number")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must contain exactly 16 digits")]
        public string CardNumber
        {
            get { return cardNumber; }
            set
            {
                cardNumber = value;
                ValidateProperty(value, "CardNumber");
            }
        }

        [Required(ErrorMessage = "You must enter card date")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{2}$", ErrorMessage = "Card date must be like MM/YY")]
        public string CardDate
        {
            get { return cardDate; }
            set
            {
                cardDate = value;
                ValidateProperty(value, "CardDate");
            }
        }

        [Required(ErrorMessage = "You must enter CVV")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVV must contain exactly 3 digits")]
        public string Cvv
        {
            get { return cvv; }
            set
            {
                cvv = value;
                ValidateProperty(value, "Cvv");
            }
        }

        [Required(ErrorMessage = "You must enter address")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Address must be between 3 and 60 characters")]
        [RegularExpression(@"^(?=.*[A-Za-zא-ת])(?=.*\d)[A-Za-zא-ת0-9 ,.'/-]+$", ErrorMessage = "Address must contain letters and numbers")]
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                ValidateProperty(value, "Address");
            }
        }
    }
}