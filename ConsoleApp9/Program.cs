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
                    if (choice < 0 || choice > 5)
                    {
                        throw new Exception("Invalid choice.");
                    }

                    switch (choice)
                    {
                        case 1:
                            consoleHelper.AddContactMenu();
                            break;


                        case 2:
                            int searchMethod = consoleService.takeAnswerInt("Пошук контакту для редагування (1 - за ID, 2 - за ім'ям):");

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
                            contactService.DeleteById(consoleService.takeAnswerInt("Введіть ID контакту для видалення:"));
                            break;


                        case 4:
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
                Console.WriteLine("!ПОМИЛКА!  Причина: " + ex.Message);
            }
        }
    }
}
