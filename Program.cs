using System;
using System.Collections.Generic;

namespace AddressBookSystem
{
    // Contact class to hold contact information
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

    // AddressBook class to store contacts
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

    // AddressBookManager class to manage multiple address books and contacts
    public class AddressBookManager
    {
        // Dictionary to hold multiple address books
        public Dictionary<string, AddressBook> addressBooks;

        public AddressBookManager()
        {
            addressBooks = new Dictionary<string, AddressBook>();
        }

        public void AddAddressBook(string bookName)
        {
            if (!addressBooks.ContainsKey(bookName))
            {
                addressBooks[bookName] = new AddressBook();
                Console.WriteLine($"Address book '{bookName}' added successfully.");
            }
            else
            {
                Console.WriteLine($"Address book '{bookName}' already exists.");
            }
        }

        public AddressBook GetAddressBook(string bookName)
        {
            if (addressBooks.ContainsKey(bookName))
            {
                return addressBooks[bookName];
            }
            else
            {
                Console.WriteLine($"Address book '{bookName}' does not exist.");
                return null;
            }
        }

        // Method to display all address books
        public void DisplayAllAddressBooks()
        {
            Console.WriteLine("Available Address Books:");
            foreach (var book in addressBooks.Keys)
            {
                Console.WriteLine(book);
            }
            Console.WriteLine(" ");
        }


        // Method to add contact to address book
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
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                }
                Console.WriteLine(" ");
            }
            addressBook.DisplayContacts();
        }


        // Method to edit contact in address book
        public void EditContact(AddressBook addressBook)
        {
            bool editContact = true;
            while (editContact)
            {
                Console.Write("Do you want to edit a contact? (yes/no): ");
                string editResponse = Console.ReadLine().ToLower();

                if (editResponse == "yes")
                {
                    Console.Write("Enter First Name of the contact you want to edit: ");
                    string firstName = Console.ReadLine();
                    Console.Write("Enter Last Name of the contact you want to edit: ");
                    string lastName = Console.ReadLine();
                    addressBook.EditContact(firstName, lastName);
                }
                else if (editResponse == "no")
                {
                    editContact = false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                }
                Console.WriteLine(" ");
            }
            addressBook.DisplayContacts();
        }


        // Method to delete contact from address book
        public void DeleteContact(AddressBook addressBook)
        {
            bool deleteContact = true;
            while (deleteContact)
            {
                Console.Write("Do you want to delete a contact? (yes/no): ");
                string deleteResponse = Console.ReadLine().ToLower();

                if (deleteResponse == "yes")
                {
                    Console.Write("Enter First Name of the contact you want to delete: ");
                    string firstName = Console.ReadLine();
                    Console.Write("Enter Last Name of the contact you want to delete: ");
                    string lastName = Console.ReadLine();
                    addressBook.DeleteContact(firstName, lastName);
                }
                else if (deleteResponse == "no")
                {
                    deleteContact = false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
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
            AddressBookManager manager = new AddressBookManager();

            while (true)
            {
                Console.WriteLine("1. Add Address Book");
                Console.WriteLine("2. Select Address Book");
                Console.WriteLine("3. Display All Address Books");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Write("Enter Address Book name: ");
                        string bookName = Console.ReadLine();
                        manager.AddAddressBook(bookName);
                        break;

                    case 2:
                        Console.Write("Enter Address Book name: ");
                        string selectedBook = Console.ReadLine();
                        AddressBook addressBook = manager.GetAddressBook(selectedBook);

                        if (addressBook != null)
                        {
                            manager.AddContact(addressBook);
                            manager.EditContact(addressBook);
                            manager.DeleteContact(addressBook);

                        }
                        break;

                    case 3:
                        manager.DisplayAllAddressBooks();
                        break;

                    case 4:
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine(" ");
            }
        }
    }
}

