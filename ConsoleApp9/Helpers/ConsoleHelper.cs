using ConsoleApp9.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9.Helpers
{
    internal class ConsoleHelper
    {
        private readonly ConsoleService _consoleService;
        private readonly ContactService _contactService;

        public ConsoleHelper(ConsoleService consoleService, ContactService contactService)
        {
            _consoleService = consoleService;
            _contactService = contactService;
        }


        public void AddContactMenu()
        {
            string name = _consoleService.takeAnswerString("Введіть ім'я контакту:");
            List<string> phoneNumbers = new List<string>();
            while (true)
            {
                string phone = _consoleService.takeAnswerString("Введіть номер телефону (або 'q' для виходу):");
                if (phone == "")
                {
                    throw new Exception("Phone number can`t be empty");
                }

                if (phone.ToLower() == "q") break;
                phoneNumbers.Add(phone);
            }
            string address = _consoleService.takeAnswerString("Введіть адресу контакту (необов'язково):");
            _contactService.AddContact(name, phoneNumbers, address);
            Console.WriteLine("Контакт успішно додано.");
        }

        public void ShowContactsMenu()
        {
            _contactService.GetAll(); 
            foreach (var contact in _contactService.GetAll())
            {
                ShowSeparator();
                Console.WriteLine($"{contact.getId()}. \nІм'я: {contact.getName()}, \nАдреса: {contact.getAddress()}");
                Console.WriteLine("Номери телефонів:");
                foreach (var phone in contact.getPhoneNumbers())
                {
                    Console.WriteLine($"- {phone}");
                }
                ShowSeparator();
                Console.WriteLine();
            }
        }

        public void UpdateContactByIdMenu(int id)
        {
            if (id < 1 || id > 2)
            {
                throw new Exception("Invalid ID.");
            }

            int answer = _consoleService.takeAnswerInt("Що ви хочете редагувати? (1 - ім'я, 2 - номери телефонів, 3 - адресу):");
            if (answer < 1 || answer > 3)
            {
                throw new Exception("Invalid choice.");
            }

            switch (answer)
            {
                case 1:
                    string newName = _consoleService.takeAnswerString("Введіть нове ім'я:");
                    _contactService.UpdateNameById(id, newName);
                    break;


                case 2:
                    List<string> newPhones = new List<string>();
                    while (true)
                    {
                        string phone = _consoleService.takeAnswerString("Введіть новий номер телефону (або 'q' для виходу):");
                        if (phone == "")
                        {
                            throw new Exception("Phone number can`t be empty");
                        }

                        if (phone.ToLower() == "q") break;
                        newPhones.Add(phone);
                    }
                    _contactService.UpdatePhoneById(id, newPhones);
                    break;


                case 3:
                    string newAddress = _consoleService.takeAnswerString("Введіть нову адресу:");
                    _contactService.UpdateAddressById(id, newAddress);
                    break;


                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }
        }

        public void UpdateContactByNameMenu(string name)
        {

            int answerName = _consoleService.takeAnswerInt("Що ви хочете редагувати? (1 - ім'я, 2 - номери телефонів, 3 - адресу):");
            if (answerName < 1 || answerName > 3)
            {
                throw new Exception("Invalid choice.");
            }

            switch (answerName)
            {
                case 1:
                    string newName = _consoleService.takeAnswerString("Введіть нове ім'я:");
                    _contactService.UpdateNameByName(name, newName);
                    break;


                case 2:
                    List<string> newPhones = new List<string>();
                    while (true)
                    {
                        string phone = _consoleService.takeAnswerString("Введіть новий номер телефону (або 'q' для виходу):");
                        if (phone == "")
                        {
                            throw new Exception("Phone number can`t be empty");
                        }

                        if (phone.ToLower() == "q") break;
                        newPhones.Add(phone);
                    }
                    _contactService.UpdatePhoneByName(name, newPhones);
                    break;


                case 3:
                    string newAddress = _consoleService.takeAnswerString("Введіть нову адресу:");
                    _contactService.UpdateAddressByName(name, newAddress);
                    break;


                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }
        }

        public void ShowSeparator()
        {
            Console.WriteLine("===================");
        }
    }
}
