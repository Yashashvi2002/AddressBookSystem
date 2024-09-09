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

        public Contacts GetContactDetailsFromUser()
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Address: ");
            string address = Console.ReadLine();

            Console.Write("Enter City: ");
            string city = Console.ReadLine();

            Console.Write("Enter State: ");
            string state = Console.ReadLine();

            Console.Write("Enter Zip: ");
            string zip = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            return new Contacts
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                City = city,
                State = state,
                Zip = zip,
                PhoneNumber = phoneNumber,
                Email = email
            };
        }

        public void EditContact(string firstName, string lastName)
        {
            bool contactFound = false;
            foreach (var contact in contactList)
            {
                if (contact.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) && contact.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
                {
                    contactFound = true;
                    Console.WriteLine("Editing contact details:");
                    Console.WriteLine($"Current First Name: {contact.FirstName}");
                    Console.Write("Enter new First Name (leave empty to keep current): ");
                    string newFirstName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newFirstName)) contact.FirstName = newFirstName;

                    Console.WriteLine($"Current Last Name: {contact.LastName}");
                    Console.Write("Enter new Last Name (leave empty to keep current): ");
                    string newLastName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newLastName)) contact.LastName = newLastName;

                    Console.WriteLine($"Current Address: {contact.Address}");
                    Console.Write("Enter new Address (leave empty to keep current): ");
                    string newAddress = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newAddress)) contact.Address = newAddress;

                    Console.WriteLine($"Current City: {contact.City}");
                    Console.Write("Enter new City (leave empty to keep current): ");
                    string newCity = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newCity)) contact.City = newCity;

                    Console.WriteLine($"Current State: {contact.State}");
                    Console.Write("Enter new State (leave empty to keep current): ");
                    string newState = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newState)) contact.State = newState;

                    Console.WriteLine($"Current Zip: {contact.Zip}");
                    Console.Write("Enter new Zip (leave empty to keep current): ");
                    string newZip = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newZip)) contact.Zip = newZip;

                    Console.WriteLine($"Current Phone Number: {contact.PhoneNumber}");
                    Console.Write("Enter new Phone Number (leave empty to keep current): ");
                    string newPhoneNumber = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newPhoneNumber)) contact.PhoneNumber = newPhoneNumber;

                    Console.WriteLine($"Current Email: {contact.Email}");
                    Console.Write("Enter new Email (leave empty to keep current): ");
                    string newEmail = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newEmail)) contact.Email = newEmail;

                    Console.WriteLine(" ");
                    Console.WriteLine("Contact details updated successfully.");
                }
                Console.WriteLine(" ");
            }
            if (!contactFound)
            {
                Console.WriteLine("Contact not found.");
            }
        }

        public void DeleteContact(string firstName, string lastName)
        {
            bool contactFound = false;
            foreach (var contact in contactList)
            {
                if (contact.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) && contact.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
                {
                    contactFound = true;
                    contactList.Remove(contact);
                    Console.WriteLine("Contact deleted successfully.");
                    break;
                }
            }
            if (!contactFound)
            {
                Console.WriteLine("Contact not found.");
            }

        }

        public void DisplayContacts()
        {
            Console.WriteLine("Contact Details:\n");
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
                Console.WriteLine(" ");
            }

        }
    }

    class AddressBookMangaer
    {
        public void AddContact(AddressBook addressBook)
        {
            bool addMoreContacts = true;
            while (addMoreContacts)
            {
                Console.Write("Do you want to add another contact? (yes/no): ");
                string response = Console.ReadLine().ToLower();

                if (response == "yes")
                {
                    Contacts newContact = addressBook.GetContactDetailsFromUser();
                    addressBook.AddContact(newContact);
                }
                else if (response == "no")
                {
                    addMoreContacts = false;
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter 'yes' or 'no'.");
                }
                Console.WriteLine(" ");
            }
            addressBook.DisplayContacts();
        }

        public void EditContact(AddressBook addressBook)
        {
            bool editContact = true;
            while (editContact)
            {
                Console.Write("Do you want to edit contact? (yes/no): ");
                string editResponse = Console.ReadLine().ToLower();
                if (editResponse == "yes")
                {
                    Console.Write("Enter First Name of contact you want to edit: ");
                    string firstName = Console.ReadLine();
                    Console.Write("Enter Last Name of contact you want to edit: ");
                    string lastName = Console.ReadLine();
                    addressBook.EditContact(firstName, lastName);
                }
                else if (editResponse == "no")
                {

                    Console.WriteLine("\nThank you for using Address Book System.");
                    editContact = false;
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter 'yes' or 'no'.");
                }
                Console.WriteLine(" ");
            }
            addressBook.DisplayContacts();
        }

        public void DeleteContact(AddressBook addressBook)
        {
            bool deleteContact = true;
            while (deleteContact)
            {
                Console.Write("Do you want to delete contact? (yes/no): ");
                string deleteResponse = Console.ReadLine().ToLower();
                if (deleteResponse == "yes")
                {
                    Console.Write("Enter First Name of contact you want to delete: ");
                    string firstName = Console.ReadLine();
                    Console.Write("Enter Last Name of contact you want to delete: ");
                    string lastName = Console.ReadLine();
                    addressBook.DeleteContact(firstName, lastName);
                }
                else if (deleteResponse == "no")
                {
                    Console.WriteLine("\nThank you for using Address Book System.");
                    deleteContact = false;
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter 'yes' or 'no'.");
                }
                Console.WriteLine(" ");
            }
            addressBook.DisplayContacts();
        }

    }

    class Program
    {
        static void Main()
        {
            AddressBook addressBook = new AddressBook();
            AddressBookMangaer manager = new AddressBookMangaer();
            Console.WriteLine("Welcome to Address Book System!");
            Console.WriteLine(" ");
            manager.AddContact(addressBook);
            manager.EditContact(addressBook);
            manager.DeleteContact(addressBook);

        }
    }
}
