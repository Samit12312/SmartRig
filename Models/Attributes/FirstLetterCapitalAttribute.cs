using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class FirstLetterCapitalAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            string text = value.ToString().Trim();

            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            char firstLetter = text[0];

            if (IsEnglishLetter(firstLetter))
            {
                if (char.IsUpper(firstLetter) == false)
                {
                    return false;
                }
            }
            else if (IsHebrewLetter(firstLetter) == false)
            {
                return false;
            }

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if (char.IsLetter(c) == false &&
                    c != ' ' &&
                    c != '-' &&
                    c != '\'')
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsEnglishLetter(char c)
        {
            return c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z';
        }

        private bool IsHebrewLetter(char c)
        {
            return c >= 'א' && c <= 'ת';
        }
    }
}