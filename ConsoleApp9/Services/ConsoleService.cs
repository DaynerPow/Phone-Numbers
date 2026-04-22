using System;

namespace ConsoleApp9.Services
{
    internal class ConsoleService
    {
        public string takeAnswerString(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }

        public int takeAnswerInt(string text)
        {
            Console.WriteLine(text);
            return Convert.ToInt32(Console.ReadLine());
        }

        public void PrintContact(Contact contact)
        {
            Console.WriteLine("ID: " + contact.getId());
            Console.WriteLine("Ім'я: " + contact.getName());
            Console.WriteLine("Номери телефонів:");
            foreach (var phone in contact.getPhoneNumbers())
            {
                Console.WriteLine("- " + phone);
            }
            Console.WriteLine("Адреса: " + contact.getAddress());
        }
    }
}
