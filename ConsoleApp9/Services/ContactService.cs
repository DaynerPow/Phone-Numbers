using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Dont use LINQ
// Зроби методи для валідації данних БІЛЬШЕ ВАЛІЛАЦІЇ!!!!!
// Дороби пошук контактів
// Голова вже не так болить, але все ж таки краще розділити код на менші методи, щоб було легше читати і підтримувати.

namespace ConsoleApp9
{
    internal class ContactService
    {
        private readonly List<Contact> contacts = new List<Contact>();
        private readonly int phoneNumberMinLength = 5, phoneNumberMaxLength = 15, userNameMinLength = 2;

        public void AddContact(string name, List<string> phoneNumbers, string address = "")
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");

            if (phoneNumbers == null || phoneNumbers.Count == 0)
                throw new ArgumentException("At least one phone number is required.");

            if (name.Length < userNameMinLength)
                throw new ArgumentException("Name must be at least " + userNameMinLength + " characters long.");

            foreach (var exsistingContact in contacts)
            {
                if(exsistingContact.getName().ToLower() == name.ToLower())
                    throw new ArgumentException("Contact with the same name already exists.");
            }

            contacts.Add(new Contact(name, new List<string>(phoneNumbers), address));
        }

        public List<Contact> GetAll()
        {
            if (contacts.Count == 0 || contacts == null)
                throw new InvalidOperationException("No contacts found.");

            return new List<Contact>(contacts);
        }

        public Contact GetById(int id)
        {
            foreach (var contact in contacts)
            {
                if (contact.getId() == id)
                {
                    return contact;
                }
            }
            throw new ArgumentException("Contact not found.");
        }

        public bool AddPhone(int contactId, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number cannot be empty.");

            if (phoneNumber.Length < phoneNumberMinLength || phoneNumber.Length > phoneNumberMaxLength)
                throw new ArgumentException("Phone number must be between " + phoneNumberMinLength + " and " + phoneNumberMaxLength + " characters long.");

            var contact = GetById(contactId);
            if (contact == null) return false;

            contact.addPhoneNumber(phoneNumber);
            return true;
        }

        public bool RemovePhone(int contactId, string PhoneNumber)
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
                throw new ArgumentException("Phone number cannot be empty.");

            var contact = GetById(contactId);
            if (contact == null) return false;

            List<string> currentPhones = contact.getPhoneNumbers();
            if (currentPhones.Count <= 1)
                throw new InvalidOperationException("A contact must have at least one phone number.");

            contact.removePhoneNumber(PhoneNumber);
            return true;
        }

        public bool UpdateNameById(int id, string NewName)
        {
            if (string.IsNullOrWhiteSpace(NewName))
                throw new ArgumentException("New name cannot be empty.");

            if (NewName.Length < userNameMinLength)
                throw new ArgumentException("New name must be at least " + userNameMinLength + " characters long.");

            var contact = GetById(id);
            if (contact == null) return false;


            contact.setName(NewName);
            return true;
        }

        public bool UpdateAddressById(int id, string NewAddress)
        {
            if (string.IsNullOrWhiteSpace(NewAddress))
                throw new ArgumentException("New address cannot be empty.");

            var contact = GetById(id);
            if (contact == null) return false;

            contact.setAddress(NewAddress);
            return true;
        }

        public bool UpdatePhoneById(int id, List<string> NewPhones)
        {
            if (NewPhones == null || NewPhones.Count == 0)
                throw new ArgumentException("At least one phone number is required.");
            foreach (var phone in NewPhones)
            {
                if (string.IsNullOrWhiteSpace(phone))
                    throw new ArgumentException("Phone number cannot be empty.");

                if (phone.Length < phoneNumberMinLength || phone.Length > phoneNumberMaxLength)
                    throw new ArgumentException("Phone number must be between " + phoneNumberMinLength + " and " + phoneNumberMaxLength + " characters long.");
            }
            var contact = GetById(id);
            if (contact == null) return false;
            contact.setPhoneNumbers(NewPhones);
            return true;
        }

        public bool UpdateNameByName(string name, string NewName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");
            if (string.IsNullOrWhiteSpace(NewName))
                throw new ArgumentException("New name cannot be empty.");
            if (NewName.Length < userNameMinLength)
                throw new ArgumentException("New name must be at least " + userNameMinLength + " characters long.");
            foreach (var contact in contacts)
            {
                if (contact.getName().Trim().ToLower() == name.Trim().ToLower())
                {
                    contact.setName(NewName);
                    return true;
                }
            }
            return false;
        }

        public bool UpdateAddressByName(string name, string NewAddress)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");
            if (string.IsNullOrWhiteSpace(NewAddress))
                throw new ArgumentException("New address cannot be empty.");
            foreach (var contact in contacts)
            {
                if (contact.getName().Trim().ToLower() == name.Trim().ToLower())
                {
                    contact.setAddress(NewAddress);
                    return true;
                }
            }
            return false;
        }

        public bool UpdatePhoneByName(string name, List<string> NewPhones)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");
            if (NewPhones == null || NewPhones.Count == 0)
                throw new ArgumentException("At least one phone number is required.");
            foreach (var phone in NewPhones)
            {
                if (string.IsNullOrWhiteSpace(phone))
                    throw new ArgumentException("Phone number cannot be empty.");
                if (phone.Length < phoneNumberMinLength || phone.Length > phoneNumberMaxLength)
                    throw new ArgumentException("Phone number must be between " + phoneNumberMinLength + " and " + phoneNumberMaxLength + " characters long.");
            }
            foreach (var contact in contacts)
            {
                if (contact.getName().Trim().ToLower() == name.Trim().ToLower())
                {
                    contact.setPhoneNumbers(NewPhones);
                    return true;
                }
            }
            return false;
        }

        public bool DeleteById(int contactId)
        {
            if (contactId < 0)
                throw new ArgumentException("Invalid contact ID.");

            var contact = GetById(contactId);
            if (contact == null) return false;

            contacts.Remove(contact);
            return true;
        }

        public List<Contact> Search(string query)
        {
            List<Contact> result = new List<Contact>();
            foreach (var contact in contacts)
            {
                if (contact.getName().ToLower().Contains(query.ToLower()))
                {
                    result.Add(contact);
                    continue;
                }

                foreach (var phone in contact.getPhoneNumbers())
                {
                    if (phone.Contains(query))
                    {
                        result.Add(contact);
                        break;
                    }
                }
            }
            return result;

        }
    }
}
