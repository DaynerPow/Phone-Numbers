using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    internal class ContactService
    {
        private List<Contact> contacts = new List<Contact>();

        public void AddContact(string name, List<string> phoneNumbers, string address = "")
        {
            contacts.Add(new Contact(name, new List<string>(phoneNumbers), address));
        }

        public List<Contact> GetAll()
        {
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
            return null;
        }

        public bool AddPhone(int contactId, string phoneNumber)
        {
            var contact = GetById(contactId);
            if (contact == null) return false;

            contact.addPhoneNumber(phoneNumber);
            return true;
        }

        public bool RemovePhone(int contactId, string PhoneNumber)
        {
            var contact = GetById(contactId);
            if (contact == null) return false;

            contact.removePhoneNumber(PhoneNumber);
            return true;
        }

        public bool UpdateById(int id, string NewName, string NewAddress)
        {
            var contact = GetById(id);
            if (contact == null) return false;

            contact.setName(NewName);
            contact.setAddress(NewAddress);
            return true;
        }

        public bool UpdateByName(string name, string NewName, string NewAddress)
        {
            foreach (var contact in contacts)
            {
                if (contact.getName().Trim().ToLower() == name.Trim().ToLower())
                {
                    contact.setName(NewName);
                    contact.setAddress(NewAddress);
                    return true;
                }
            }
            return false;
        }

        public bool DeleteById(int contactId)
        {
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
