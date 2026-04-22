using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp9
{
    internal static class SaveLoad
    {
        private const string FilePath = "contacts.json";

        public static void Save(List<Contact> contacts)
        {
            try
            {
                string json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("!ПОМИЛКА ЗБЕРЕЖЕННЯ! Причина: " + ex.Message);
            }
        }

        public static List<Contact> Load()
        {
            try
            {
                if (!File.Exists("contacts.json"))
                    return new List<Contact>();

                string json = File.ReadAllText("contacts.json");

                var result = JsonConvert.DeserializeObject<List<Contact>>(json);

                return result ?? new List<Contact>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("!ПОМИЛКА ЗАВАНТАЖЕННЯ! Причина: " + ex.Message);
                return new List<Contact>();
            }
        }
    }
}
