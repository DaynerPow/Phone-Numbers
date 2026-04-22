using System;

namespace ConsoleApp9.Services
{
    internal class ValidatorService
    {
        public void ValidateAnswerSwitch(int answer, int min, int max)
        {
            if (answer < min || answer > max)
            {
                throw new Exception("Invalid choice.");
            }
        }

        public void ValidateIsEmpty(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }
        }

        public void ValidatePhoneNumber(string phoneNumber, int minLength, int maxLength)
        {

            if (phoneNumber.Length < minLength || phoneNumber.Length > maxLength)
                throw new ArgumentException("Phone number must be between " + minLength + " and " + maxLength + " characters long.");
            if (!IsValidPhone(phoneNumber))
                throw new ArgumentException("Номер телефону містить недопустимі символи (дозволено лише цифри, +, #, *).");
        }

        public void ValidateName(string name, int minLength)
        {
            if (name.Length < minLength)
                throw new ArgumentException("Name must be at least " + minLength + " characters long.");
        }

        public bool IsValidPhone(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c) && c != '+' && c != '#' && c != '*')
                    return false;
            }

            return true;
        }
    }
}
