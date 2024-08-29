using System;
using System.Collections.Generic;

namespace AddressBookSystem
{
    public class Contacts
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }

    public class AddressBook
    {
        public List<Contacts> contactList;
        public AddressBook()
        {
            contactList = new List<Contacts>();
        }

        public void AddContact(Contacts contact)
        {
            contactList.Add(contact);
        }

        public void DisplayContacts()
        {
            foreach (var contact in contactList)
            {
                Console.WriteLine($"First Name: {contact.FirstName}");
                Console.WriteLine($"Last Name: {contact.LastName}");
                Console.WriteLine($"Address:  {contact.Address}");
                Console.WriteLine($"City: {contact.City} ");
                Console.WriteLine($"State: {contact.State}");
                Console.WriteLine($"Zip: {contact.Zip}");
                Console.WriteLine($"Phone Number: {contact.PhoneNumber}");
                Console.WriteLine($"Email: {contact.Email}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AddressBook addressBook = new AddressBook();
            Contacts contact = new Contacts
            {
                FirstName = "Nikhil",
                LastName = "Dev",
                Address = "Chembur",
                City = "Mumbai",
                State = "Maharashtra",
                Zip = "400071",
                PhoneNumber = "1234567890",
                Email = "nikdev123.com",
            };
            addressBook.AddContact(contact);
            addressBook.DisplayContacts();

            
        }
    }
}
