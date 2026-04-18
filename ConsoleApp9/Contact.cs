using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    internal class Contact
    {
            private int id = 0;
            private static int autoInc = 1;
            private string name = "";
            private List<string> phoneNumbers = new List<string>();
            private string address = "";

            public Contact(string name, List<string> phoneNumbers, string address = "")
            {
                this.id = autoInc++;
                this.name = name;
                this.phoneNumbers = phoneNumbers;
                this.address = address;
            }

            public int getId() { return id; }

            public string getName() { return name; }

            public void setName(string name)
            {
                if (name.Trim().Length != 0)
                {
                    this.name = name;
                }
            }

            public string getAddress() { return address; }
            public void setAddress(string address)
            {
                if (address.Trim().Length != 0)
                {
                    this.address = address;
                }
            }

            public void addPhoneNumber(string phoneNumber)
            {
                if (phoneNumber.Trim().Length != 0)
                {
                    this.phoneNumbers.Add(phoneNumber);
                }
            }

            public void removePhoneNumber(string phoneNumber)
            {
                this.phoneNumbers.Remove(phoneNumber);
            }

            public List<string> getPhoneNumbers()
            {
                return phoneNumbers; 
            }
        
    }
}
