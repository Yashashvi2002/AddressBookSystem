using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;

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

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}, Address: {Address}, City: {City}, State: {State}, Zip: {Zip}, Phone: {PhoneNumber}, Email: {Email}";
        }



        // Override Equals method to compare two Contacts by their first and last names
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Contacts other = (Contacts)obj;
            return FirstName.Equals(other.FirstName, StringComparison.OrdinalIgnoreCase) &&
                   LastName.Equals(other.LastName, StringComparison.OrdinalIgnoreCase);
        }

        // Override GetHashCode method for better performance in collections
        public override int GetHashCode()
        {
            return FirstName.ToLower().GetHashCode() ^ LastName.ToLower().GetHashCode();
        }
    }

    // AddressBook class to store contacts
    public class AddressBook
    {
        private Dictionary<string, List<Contacts>> cityToContacts = new Dictionary<string, List<Contacts>>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, List<Contacts>> stateToContacts = new Dictionary<string, List<Contacts>>(StringComparer.OrdinalIgnoreCase);

        public string Name { get; set; }
        public List<Contacts> contactList;
        public AddressBook(string name)
        {
            Name = name;
            contactList = new List<Contacts>();
        }

        public void AddContact(Contacts contact)
        {
            // To check if contact with the same first and last name already exists
            bool contactExists = contactList.Exists(c => c.Equals(contact));

            if (contactExists)
            {
                Console.WriteLine("A contact with the same name already exists. Duplicate entries are not allowed.");
            }
            else
            {
                contactList.Add(contact);
                Console.WriteLine("Contact added successfully.");
            }

            // Add to city dictionary
            if (!cityToContacts.ContainsKey(contact.City))
            {
                cityToContacts[contact.City] = new List<Contacts>();
            }
            cityToContacts[contact.City].Add(contact);

            // Add to state dictionary
            if (!stateToContacts.ContainsKey(contact.State))
            {
                stateToContacts[contact.State] = new List<Contacts>();
            }
            stateToContacts[contact.State].Add(contact);

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
            // Temporary contact for comparison
            Contacts tempContact = new Contacts
            {
                FirstName = firstName,
                LastName = lastName
            };

            bool contactFound = false;

            foreach (var contact in contactList)
            {
                if (contact.Equals(tempContact))
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
                    if (!string.IsNullOrEmpty(newCity))
                    {
                        // Remove the contact from the old city list
                        cityToContacts[contact.City].Remove(contact);
                        if (cityToContacts[contact.City].Count == 0)
                        {
                            cityToContacts.Remove(contact.City);
                        }

                        contact.City = newCity;

                        // Add the contact to the new city list
                        if (!cityToContacts.ContainsKey(newCity))
                        {
                            cityToContacts[newCity] = new List<Contacts>();
                        }
                        cityToContacts[newCity].Add(contact);
                    }


                    Console.WriteLine($"Current State: {contact.State}");
                    Console.Write("Enter new State (leave empty to keep current): ");
                    string newState = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newState))
                    {
                        // Remove the contact from the old state list
                        stateToContacts[contact.State].Remove(contact);
                        if (stateToContacts[contact.State].Count == 0)
                        {
                            stateToContacts.Remove(contact.State);
                        }

                        contact.State = newState;

                        // Add the contact to the new state list
                        if (!stateToContacts.ContainsKey(newState))
                        {
                            stateToContacts[newState] = new List<Contacts>();
                        }
                        stateToContacts[newState].Add(contact);
                    }

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

        public void SortContactsByName()
        {
            contactList = contactList.OrderBy(c => c.FirstName).ThenBy(c => c.LastName).ToList();
            Console.WriteLine("Contacts sorted by name.");
            DisplayContacts();
        }

        public void SortContactsByCity()
        {
            contactList = contactList.OrderBy(c => c.City).ToList();
            Console.WriteLine("Contacts sorted by city.");
            DisplayContacts();
        }

        public void SortContactsByState()
        {
            contactList = contactList.OrderBy(c => c.State).ToList();
            Console.WriteLine("Contacts sorted by state.");
            DisplayContacts();
        }

        public void SortContactsByZip()
        {
            contactList = contactList.OrderBy(c => c.Zip).ToList();
            Console.WriteLine("Contacts sorted by zip.");
            DisplayContacts();
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
                addressBooks[bookName] = new AddressBook(bookName);
                Console.WriteLine($"Address book '{bookName}' added successfully.");
            }
            else
            {
                Console.WriteLine($"Address book '{bookName}' already exists.");
            }
        }

        public AddressBook GetAddressBook(string bookName)
        {
            var key = addressBooks.Keys.FirstOrDefault(k => k.Equals(bookName, StringComparison.OrdinalIgnoreCase));
            if (key != null)
            {
                return addressBooks[key];
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
        public void AddContact(AddressBook addressBook, string filePath)
        {
            bool addMoreContacts = true;
            while (addMoreContacts)
            {
                Console.Write("Do you want to add contact? (yes/no): ");
                string response = Console.ReadLine().ToLower();

                if (response == "yes")
                {
                    Contacts newContact = addressBook.GetContactDetailsFromUser();
                    addressBook.AddContact(newContact);
                    AddressBookFileIO.WriteAddressBooksToFile(addressBooks, filePath);
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
            Console.WriteLine("Contacts Added Successfully...!");
            addressBook.DisplayContacts();
        }


        // Method to edit contact in address book
        public void EditContact(AddressBook addressBook, string filePath)
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
                    AddressBookFileIO.WriteAddressBooksToFile(addressBooks, filePath);
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
            Console.WriteLine("Contacts Edited Successfully...!");
            addressBook.DisplayContacts();
        }


        // Method to delete contact from address book
        public void DeleteContact(AddressBook addressBook, string filePath)
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
                    AddressBookFileIO.WriteAddressBooksToFile(addressBooks, filePath);
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

        // Method to search for a person in all address books by city or state
        public void SearchPersonByCityOrState(string cityOrState)
        {
            bool personFound = false;
            foreach (var addressBook in addressBooks.Values)
            {
                foreach (var contact in addressBook.contactList)
                {
                    if (contact.City.Equals(cityOrState, StringComparison.OrdinalIgnoreCase) || contact.State.Equals(cityOrState, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Contact found in Address Book {addressBook.Name} :");
                        Console.WriteLine($"First Name: {contact.FirstName}");
                        Console.WriteLine($"Last Name: {contact.LastName}");
                        Console.WriteLine($"Address: {contact.Address}");
                        Console.WriteLine($"City: {contact.City}");
                        Console.WriteLine($"State: {contact.State}");
                        Console.WriteLine($"Zip: {contact.Zip}");
                        Console.WriteLine($"Phone Number: {contact.PhoneNumber}");
                        Console.WriteLine($"Email: {contact.Email}");
                        Console.WriteLine(" ");
                        personFound = true;
                    }
                }
            }
            if (!personFound)
            {
                Console.WriteLine("No contacts found in the given city or state.");
            }
        }

        //Method to count contact by city or state in all addressbook
        public void CountByCityOrState(string cityOrState)
        {
            int count = 0;
            foreach (var addressBook in addressBooks.Values)
            {
                foreach (var contact in addressBook.contactList)
                {
                    if (contact.City.Equals(cityOrState, StringComparison.OrdinalIgnoreCase) || contact.State.Equals(cityOrState, StringComparison.OrdinalIgnoreCase))
                    {
                        count++;
                    }
                }
            }
            Console.WriteLine($"Number of contacts in {cityOrState}: {count}");
        }

        //Method to sort contact by name in addressbook
        public void SortContactsByName(AddressBook addressBook)
        {
            if (addressBook != null)
            {
                addressBook.SortContactsByName();
            }
            else
            {
                Console.WriteLine("Address book not found.");
            }
        }

        //Method to sort contact by city in addressbook
        public void SortContactsByCity(AddressBook addressBook)
        {
            if (addressBook != null)
            {
                addressBook.SortContactsByCity();
            }
            else
            {
                Console.WriteLine("Address book not found.");
            }
        }


        //Method to sort contact by state in addressbook
        public void SortContactByState(AddressBook addressBook)
        {
            if (addressBook != null)
            {
                addressBook.SortContactsByState();
            }
            else
            {
                Console.WriteLine("Address book not found.");
            }
        }

        //Method to sort contact by zip in addressbook
        public void SortContactsByZip(AddressBook addressBook)
        {
            if (addressBook != null)
            {
                addressBook.SortContactsByZip();
            }
            else
            {
                Console.WriteLine("Address book not found.");
            }
        }
    }

    //Class to read and write AddressBook to a file
    public static class AddressBookFileIO
    {
        // Write AddressBook with its name to a file
        public static void WriteAddressBooksToFile(Dictionary<string, AddressBook> addressBooks, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var addressBook in addressBooks.Values)
                {
                    writer.WriteLine($"AddressBook: {addressBook.Name}");

                    foreach (var contact in addressBook.contactList)
                    {
                        writer.WriteLine($"{contact.FirstName},{contact.LastName},{contact.Address},{contact.City},{contact.State},{contact.Zip},{contact.PhoneNumber},{contact.Email}");
                    }
                    writer.WriteLine("EndOfAddressBook\n");
                }
            }
        }

        // Read AddressBook from a file
        public static void ReadAddressBooksFromFile(Dictionary<string, AddressBook> addressBooks, string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                AddressBook currentBook = null;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("AddressBook:"))
                    {
                        string bookName = line.Substring("AddressBook:".Length).Trim();
                        currentBook = new AddressBook(bookName);
                        addressBooks[bookName] = currentBook;
                    }
                    else if (line == "EndOfAddressBook")
                    {
                        currentBook = null;
                    }
                    else if (currentBook != null)
                    {
                        string[] contactData = line.Split(',');
                        if (contactData.Length == 8)
                        {
                            Contacts contact = new Contacts
                            {
                                FirstName = contactData[0],
                                LastName = contactData[1],
                                Address = contactData[2],
                                City = contactData[3],
                                State = contactData[4],
                                Zip = contactData[5],
                                PhoneNumber = contactData[6],
                                Email = contactData[7]
                            };
                            currentBook.contactList.Add(contact);
                        }
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            AddressBookManager manager = new AddressBookManager();

            // File path to store address book data
            string folderPath = @"C:\Users\Lenovo\OneDrive\Desktop\.NET FOLDER\AddressBookSystem";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine("Directory did not exist, so it was created.");
            }

            Console.Write("Enter the name for the Address Book file (without extension): ");
            string fileName = Console.ReadLine();

            string filePath = Path.Combine(folderPath, $"{fileName}.txt");

            if (!File.Exists(filePath))
            {
                
                using (File.Create(filePath))
                {
                    Console.WriteLine($"File '{fileName}.txt' did not exist, so a new file was created at {filePath}.");
                }
            }
            else
            {
                Console.WriteLine($"File '{fileName}.txt' already exists at {filePath}.");
            }
            AddressBookFileIO.ReadAddressBooksFromFile(manager.addressBooks, filePath);

            bool moreOperations = true;
            while (moreOperations)
            {
                AddressBookFileIO.WriteAddressBooksToFile(manager.addressBooks, filePath);
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. Add Address Book");
                Console.WriteLine("2. Add Contact");
                Console.WriteLine("3. Edit Contact");
                Console.WriteLine("4. Delete Contact");
                Console.WriteLine("5. Search Person by City or State");
                Console.WriteLine("6. Count Person by City or State");
                Console.WriteLine("7. Sort the Contacts");
                Console.WriteLine("8. Display All Address Books");
                Console.WriteLine("9. Load Address Book from File");
                Console.WriteLine("10. Exit");
                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(" ");

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter the name of the Address Book you want to add: ");
                        string bookName = Console.ReadLine();
                        manager.AddAddressBook(bookName);
                        break;

                    case 2:
                        manager.DisplayAllAddressBooks();
                        Console.Write("Enter the name of the Address Book you want to add contact to: ");
                        string addressBookName = Console.ReadLine();
                        AddressBook addressBook = manager.GetAddressBook(addressBookName);
                        if (addressBook != null)
                        {
                            manager.AddContact(addressBook, filePath);
                        }

                        break;

                    case 3:
                        manager.DisplayAllAddressBooks();
                        Console.Write("Enter the name of the Address Book you want to edit contact in: ");
                        string editBookName = Console.ReadLine();
                        AddressBook editAddressBook = manager.GetAddressBook(editBookName);
                        if (editAddressBook != null)
                        {
                            manager.EditContact(editAddressBook, filePath);
                            
                        }
                        break;

                    case 4:
                        manager.DisplayAllAddressBooks();
                        Console.Write("Enter the name of the Address Book you want to delete contact from: ");
                        string deleteBookName = Console.ReadLine();
                        AddressBook deleteAddressBook = manager.GetAddressBook(deleteBookName);
                        if (deleteAddressBook != null)
                        {
                            manager.DeleteContact(deleteAddressBook,filePath);
                            AddressBookFileIO.WriteAddressBooksToFile(manager.addressBooks, filePath);
                        }
                        break;

                    case 5:
                        Console.Write("Enter the city or state to search for a person: ");
                        string cityOrState = Console.ReadLine();
                        break;

                    case 6:
                        Console.Write("Enter the city or state to count contacts: ");
                        string cityOrStateToCount = Console.ReadLine();
                        break;

                    case 7:
                        manager.DisplayAllAddressBooks();
                        // Sorting criteria
                        Console.WriteLine("Choose sorting criteria:");
                        Console.WriteLine("1. Sort by Name");
                        Console.WriteLine("2. Sort by City");
                        Console.WriteLine("3. Sort by State");
                        Console.WriteLine("4. Sort by Zip");
                        Console.Write("Enter your choice: ");
                        int sortChoice = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter the name of the Address Book you want to sort contacts in: ");
                        string sortBookName = Console.ReadLine();
                        AddressBook sortAddressBook = manager.GetAddressBook(sortBookName);
                        if (sortAddressBook != null)
                        {
                            switch (sortChoice)
                            {
                                case 1:
                                    manager.SortContactsByName(sortAddressBook);
                                    AddressBookFileIO.WriteAddressBooksToFile(manager.addressBooks, filePath);
                                    break;

                                case 2:
                                    manager.SortContactsByCity(sortAddressBook);
                                    AddressBookFileIO.WriteAddressBooksToFile(manager.addressBooks, filePath);
                                    break;

                                case 3:
                                    manager.SortContactByState(sortAddressBook);
                                    AddressBookFileIO.WriteAddressBooksToFile(manager.addressBooks, filePath);
                                    break;

                                case 4:
                                    manager.SortContactsByZip(sortAddressBook);
                                    AddressBookFileIO.WriteAddressBooksToFile(manager.addressBooks, filePath);
                                    break;

                                default:
                                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                                    break;
                            }
                        }
                        break;

                    case 8:
                        manager.DisplayAllAddressBooks();
                        break;

                    case 9:
                        Console.Write("Enter the name of the Address Book to display contacts: ");
                        string displayBookName = Console.ReadLine();
                        AddressBook displayAddressBook = manager.GetAddressBook(displayBookName);
                        if (displayAddressBook != null)
                        {
                            displayAddressBook.DisplayContacts();
                        }
                        else
                        {
                            Console.WriteLine("Address Book not found.");
                        }
                        break;

                    case 10:
                        moreOperations = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 10.");
                        break;
                }
                Console.WriteLine(" ");
               
            }
        }
    }
}


