using ConsoleApp9.Helpers;
using ConsoleApp9.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    internal class Program
    {
        public void Menu()
        {
                ConsoleService consoleService = new ConsoleService();
                ContactService contactService = new ContactService();
                ConsoleHelper consoleHelper = new ConsoleHelper(consoleService, contactService);
                ValidatorService validatorService = new ValidatorService();

            while (true)
            {
                try
                {
                    Console.WriteLine("МЕНЮ");
                    Console.WriteLine("1. Додати контакт");
                    Console.WriteLine("2. Редагувати контакт");
                    Console.WriteLine("3. Видалити контакт");
                    Console.WriteLine("4. Знайти контакт");
                    Console.WriteLine("5. Показати всі контакти");
                    Console.WriteLine("0. Вихід");
                    Console.Write("Ваш вибір: ");

                    int choice = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    validatorService.ValidateAnswerSwitch(choice, 0, 5);

                    switch (choice)
                    {
                        case 1:
                            consoleHelper.AddContactMenu();
                            break;


                        case 2:
                            int searchMethod = consoleService.takeAnswerInt("Пошук контакту для редагування (1 - за ID, 2 - за ім'ям):");
                            validatorService.ValidateAnswerSwitch(searchMethod, 1, 2);

                            switch (searchMethod)
                                {
                                    case 1:
                                        int id = consoleService.takeAnswerInt("Введіть ID контакту:");
                                        consoleHelper.UpdateContactByIdMenu(id);
                                        break;

                                    case 2:
                                        string searchName = consoleService.takeAnswerString("Введіть ім'я контакту:");
                                        consoleHelper.UpdateContactByNameMenu(searchName);
                                        break;

                                    default:
                                        Console.WriteLine("!Невірний вибір!");
                                        break;
                                }
                            break;


                        case 3:
                            int idToDelete = consoleService.takeAnswerInt("Введіть ID контакту для видалення:");

                            if (contactService.DeleteById(idToDelete))
                            {
                                Console.WriteLine("Контакт успішно видалено.");
                            }
                            else
                            {
                                Console.WriteLine("Помилка: Контакт з таким ID не знайдено.");
                            }
                            break;


                        case 4:
                           consoleHelper.SearchContactMenu();
                            break;

                        case 5:
                            consoleHelper.ShowContactsMenu();
                            break;


                        case 0:
                            Console.WriteLine("До побачення!");
                            return;


                        default:
                            throw new Exception("Invalid choice.");
                    }

                    Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("!Error!  \nReason: " + ex.Message);
                    Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            try
            {
                Program program = new Program();
                program.Menu();
            }
            catch (Exception ex)
            {
                Console.WriteLine("!Error!  \nReason: " + ex.Message);
                Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                Console.ReadKey();
            }
        }
    }
}
