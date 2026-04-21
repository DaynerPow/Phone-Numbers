using ConsoleApp9.Services;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

// Dont use LINQ
// Валідація номера телефону недоробна, потрібно перевіряти на допустимі символи (дозволено лише цифри, +, #, *), а також на довжину (від 5 до 15 символів).

namespace ConsoleApp9
{
    internal class ContactService
    {
        private readonly ValidatorService _validatorService = new ValidatorService();
        private readonly List<Contact> contacts;
        private const int phoneNumberMinLength = 5, phoneNumberMaxLength = 15, userNameMinLength = 2;

        public ContactService()
        {
            contacts = SaveLoad.Load();

            if (contacts.Count > 0)
            {
                int maxId = 0;
                foreach (var contact in contacts)
                {
                    if (contact.getId() > maxId)
                    {
                        maxId = contact.getId();
                    }
                }
                Contact.SetAutoInc(maxId + 1);
            }
        }

        public void AddContact(string name, List<string> phoneNumbers, string address = "")
        {
            _validatorService.ValidateIsEmpty(name);

            if (phoneNumbers == null || phoneNumbers.Count == 0)
                throw new ArgumentException("At least one phone number is required.");
            foreach (var phone in phoneNumbers)
            {
                if (!_validatorService.IsValidPhone(phone))
                {
                    throw new ArgumentException("Номер телефону містить недопустимі символи (дозволено лише цифри, +, #, *).");
                }
            }

            _validatorService.ValidateName(name, userNameMinLength);

            foreach (var exsistingContact in contacts)
            {
                if (exsistingContact.getName().ToLower() == name.ToLower())
                    throw new ArgumentException("Contact with the same name already exists.");
            }

            contacts.Add(new Contact(name, new List<string>(phoneNumbers), address));
            SaveLoad.Save(contacts);
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
            throw null;
        }

        public bool AddPhone(int contactId, string phoneNumber)
        {
            _validatorService.ValidateIsEmpty(phoneNumber);

            _validatorService.ValidatePhoneNumber(phoneNumber, phoneNumberMinLength, phoneNumberMaxLength);


            var contact = GetById(contactId);
            if (contact == null) return false;

            contact.addPhoneNumber(phoneNumber);
            SaveLoad.Save(contacts);
            return true;
        }

        public bool RemovePhone(int contactId, string PhoneNumber)
        {
            _validatorService.ValidateIsEmpty(PhoneNumber);

            var contact = GetById(contactId);
            if (contact == null) return false;

            List<string> currentPhones = contact.getPhoneNumbers();
            if (currentPhones.Count <= 1)
                throw new InvalidOperationException("A contact must have at least one phone number.");

            contact.removePhoneNumber(PhoneNumber);
            SaveLoad.Save(contacts);
            return true;
        }

        public bool UpdateNameById(int id, string NewName)
        {
            _validatorService.ValidateIsEmpty(NewName);

            _validatorService.ValidateName(NewName, userNameMinLength);

            var contact = GetById(id);
            if (contact == null) return false;


            contact.setName(NewName);
            SaveLoad.Save(contacts);
            return true;
        }

        public bool UpdateAddressById(int id, string NewAddress)
        {
            _validatorService.ValidateIsEmpty(NewAddress);

            var contact = GetById(id);
            if (contact == null) return false;

            contact.setAddress(NewAddress);
            SaveLoad.Save(contacts);
            return true;
        }

        public bool UpdatePhoneById(int id, List<string> NewPhones)
        {
            if (NewPhones == null || NewPhones.Count == 0)
                throw new ArgumentException("At least one phone number is required.");
            foreach (var phone in NewPhones)
            {
                _validatorService.ValidateIsEmpty(phone);

                _validatorService.ValidatePhoneNumber(phone, phoneNumberMinLength, phoneNumberMaxLength);
            }
            var contact = GetById(id);
            if (contact == null) return false;
            contact.setPhoneNumbers(NewPhones);
            SaveLoad.Save(contacts);
            return true;
        }

        public bool UpdateNameByName(string name, string NewName)
        {
            _validatorService.ValidateIsEmpty(name);
            _validatorService.ValidateIsEmpty(NewName);

            _validatorService.ValidateName(NewName, userNameMinLength);

            foreach (var contact in contacts)
            {
                if (contact.getName().Trim().ToLower() == name.Trim().ToLower())
                {
                    contact.setName(NewName);
                    SaveLoad.Save(contacts);    
                    return true;
                }
            }
            return false;
        }

        public bool UpdateAddressByName(string name, string NewAddress)
        {
            _validatorService.ValidateIsEmpty(name);

            _validatorService.ValidateIsEmpty(NewAddress);

            foreach (var contact in contacts)
            {
                if (contact.getName().Trim().ToLower() == name.Trim().ToLower())
                {
                    contact.setAddress(NewAddress);
                    SaveLoad.Save(contacts);
                    return true;
                }
            }
            return false;
        }

        public bool UpdatePhoneByName(string name, List<string> NewPhones)
        {
            _validatorService.ValidateIsEmpty(name);

            if (NewPhones == null || NewPhones.Count == 0)
                throw new ArgumentException("At least one phone number is required.");
            foreach (var phone in NewPhones)
            {
                _validatorService.ValidateIsEmpty(phone);

                _validatorService.ValidatePhoneNumber(phone, phoneNumberMinLength, phoneNumberMaxLength);
            }
            foreach (var contact in contacts)
            {
                if (contact.getName().Trim().ToLower() == name.Trim().ToLower())
                {
                    contact.setPhoneNumbers(NewPhones);
                    SaveLoad.Save(contacts);
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
            SaveLoad.Save(contacts);
            return true;
        }

        public List<Contact> Search(string query)
        {
            List<Contact> foundContacts = new List<Contact>();

            if (string.IsNullOrWhiteSpace(query))
                return foundContacts;

            string lowerQuery = query.Trim().ToLower();

            foreach (var contact in contacts)
            {
                bool nameMatches = contact.getName().ToLower().Contains(lowerQuery);

                bool phoneMatches = false;
                foreach (var phone in contact.getPhoneNumbers())
                {
                    if (phone.Contains(query.Trim()))
                    {
                        phoneMatches = true;
                        break;
                    }
                }

                if (nameMatches || phoneMatches)
                {
                    foundContacts.Add(contact);
                }
            }

            return foundContacts;
        }

        
    }
}
