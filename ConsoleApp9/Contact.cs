using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp9
{
    internal class Contact
    {
            [JsonProperty]
            private int id = 0;
            private static int autoInc = 1;
            [JsonProperty]
            private string name = "";
            [JsonProperty]
            private List<string> phoneNumbers = new List<string>();
            [JsonProperty]
            private string address = "";

        [JsonConstructor]
        private Contact() { }

        public Contact(string name, List<string> phoneNumbers, string address = "")
            {
                this.id = autoInc++;
                this.setName(name);
                this.phoneNumbers = new List<string>(phoneNumbers);
                this.address = address;
            }

            public int getId() { return id; }

            public string getName() { return name; }

            public void setName(string name)
            {
                if (!string.IsNullOrEmpty(name))
                    if (name.Trim().Length > 2)
                    {
                        this.name = name.Trim();
                    }
                else throw new ArgumentException("Invalid name.");
            }

        public void setPhoneNumbers(List<string> phoneNumbers)
        {
            if (phoneNumbers == null || phoneNumbers.Count == 0)
                throw new ArgumentException("At least one phone number is required.");
            this.phoneNumbers = phoneNumbers; 
        }

            public string getAddress() { return address; }
            public void setAddress(string address)
            {
                if (address != null)
                    if (address.Trim().Length > 5)
                    {
                        this.address = address.Trim();
                    }
                else throw new ArgumentException("Invalid address.");
            }

            public void addPhoneNumber(string phoneNumber)
            {
                if (phoneNumber.Trim().Length != 0 && phoneNumber != null)
                {
                    this.phoneNumbers.Add(phoneNumber);
                }
                else throw new ArgumentException("Invalid phone number.");
        }

            public void removePhoneNumber(string phoneNumber)
            {
                if (phoneNumber.Trim().Length != 0 && phoneNumber != null)
                {
                    foreach (var exsistingPhoneNumber in phoneNumbers)
                    {
                        if (exsistingPhoneNumber == phoneNumber)
                        {
                             this.phoneNumbers.Remove(phoneNumber);
                            return;
                        }
                    }
                }
                else throw new ArgumentException("Invalid phone number.");
            }

            public List<string> getPhoneNumbers()
            {
                if (this.phoneNumbers.Count > 0 && this.phoneNumbers != null)
                {
                    return phoneNumbers;
                }
                else throw new ArgumentException("Invalid phone numbers.");
            }

        public static void SetAutoInc(int nextId)
        {
            autoInc = nextId;
        }

    }
}
