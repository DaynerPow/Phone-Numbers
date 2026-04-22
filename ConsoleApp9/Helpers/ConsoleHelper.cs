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
        private readonly ValidatorService _validatorService = new ValidatorService();

        public ConsoleHelper(ConsoleService consoleService, ContactService contactService)
        {
            _consoleService = consoleService;
            _contactService = contactService;
        }

        public void AddContactMenu()
        {
            string name = _consoleService.takeAnswerString("Введіть ім'я контакту:");
            List<string> phoneNumbers = new List<string>();
            AddingPhoneNumbersMenu(phoneNumbers);

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

            int answer = _consoleService.takeAnswerInt("Що ви хочете редагувати? (1 - ім'я, 2 - номери телефонів, 3 - адресу) 0 - вихід:");
            _validatorService.ValidateAnswerSwitch(answer, 0, 3);

            switch (answer)
            {
                case 0:
                    return;

                case 1:
                    string newName = _consoleService.takeAnswerString("Введіть нове ім'я:");
                    _contactService.UpdateNameById(id, newName);
                    break;


                case 2:
                    List<string> newPhones = new List<string>();
                    AddingPhoneNumbersMenu(newPhones);

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

            int answerName = _consoleService.takeAnswerInt("Що ви хочете редагувати? (1 - ім'я, 2 - номери телефонів, 3 - адресу) 0 - вихід:");
            _validatorService.ValidateAnswerSwitch(answerName, 0, 3);

            switch (answerName)
            {
                case 0:
                    return;

                case 1:
                    string newName = _consoleService.takeAnswerString("Введіть нове ім'я:");
                    _contactService.UpdateNameByName(name, newName);
                    break;


                case 2:
                    List<string> newPhones = new List<string>();
                    AddingPhoneNumbersMenu(newPhones);

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

        public void SearchContactMenu()
        {
            Console.WriteLine("\nОберіть тип пошуку:");
            Console.WriteLine("1 - За ім'ям");
            Console.WriteLine("2 - За номером телефону");
            Console.Write("Ваш вибір: ");

            int searchType = Convert.ToInt32(Console.ReadLine());
            _validatorService.ValidateAnswerSwitch(searchType, 1, 2);

            Console.Write("Введіть текст для пошуку: ");
            string criterion = Console.ReadLine();

            List<Contact> allResults = _contactService.Search(criterion);
            bool foundAny = false;

            Console.WriteLine("\n--- Результати пошуку ---");

            switch (searchType)
            {
                case 1:
                    foreach (var c in allResults)
                    {
                        if (c.getName().ToLower().Contains(criterion.ToLower()))
                        {
                            _consoleService.PrintContact(c);
                            foundAny = true;
                        }
                    }
                    break;

                case 2:
                    foreach (var c in allResults)
                    {
                        foreach (var phone in c.getPhoneNumbers())
                        {
                            if (phone.Contains(criterion))
                            {
                                _consoleService.PrintContact(c);
                                foundAny = true;
                                break;
                            }
                        }
                    }
                    break;

                default:
                    Console.WriteLine("Помилка: Невірний варіант пошуку.");
                    foundAny = true;
                    break;
            }

            if (!foundAny)
            {
                Console.WriteLine("Нічого не знайдено за вашим запитом.");
            }
        }

        public void ShowSeparator()
        {
            Console.WriteLine("===================");
        }

        public void AddingPhoneNumbersMenu(List<string> phoneNumbers)
        {
            while (true)
            {
                string phone = _consoleService.takeAnswerString("Введіть номер телефону (або 'q' для виходу):");
                _validatorService.ValidatePhoneNumber(phone, 5, 15);
                if (phone == "")
                {
                    throw new Exception("Phone number can`t be empty");
                }

                if (phone.ToLower() == "q") break;
                phoneNumbers.Add(phone);
            }
        }
    }
}
