using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookSystem
{
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




        // Method to add contact to address book to different file formats
        public void AddContact(AddressBook addressBook, string filePath, string csvFilePath, string jsonFilePath, string selectedFormat)
        {
            bool addMoreContacts = true;
            while (addMoreContacts)
            {
                Console.Write("Do you want to add a contact? (yes/no): ");
                string response = Console.ReadLine().ToLower();

                if (response == "yes")
                {
                    Contacts newContact = addressBook.GetContactDetailsFromUser();
                    addressBook.AddContact(newContact);

                    switch (selectedFormat.ToLower())
                    {
                        case "txt":
                            AddressBookFileIO.WriteAddressBooksToFile(addressBooks, filePath);
                            Console.WriteLine("Contact saved to TXT file.");
                            break;
                        case "csv":
                            AddressBookCSVFile.WriteAddressBooksToCsv(addressBooks, csvFilePath);
                            Console.WriteLine("Contact saved to CSV file.");
                            break;
                        case "json":
                            AddressBookJSONFile.WriteAddressBooksToJson(addressBooks, jsonFilePath);
                            Console.WriteLine("Contact saved to JSON file.");
                            break;
                        case "all":
                            AddressBookFileIO.WriteAddressBooksToFile(addressBooks, filePath);
                            AddressBookCSVFile.WriteAddressBooksToCsv(addressBooks, csvFilePath);
                            AddressBookJSONFile.WriteAddressBooksToJson(addressBooks, jsonFilePath);
                            Console.WriteLine("Contact saved to all formats.");
                            break;
                        default:
                            Console.WriteLine("Invalid file format selected.");
                            break;
                    }
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
        public void EditContact(AddressBook addressBook, string filePath, string csvFilePath, string jsonFilePath, string selectedFormat)
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

                    switch (selectedFormat.ToLower())
                    {
                        case "txt":
                            AddressBookFileIO.WriteAddressBooksToFile(addressBooks, filePath);
                            Console.WriteLine("Changes saved to TXT file.");
                            break;
                        case "csv":
                            AddressBookCSVFile.WriteAddressBooksToCsv(addressBooks, csvFilePath);
                            Console.WriteLine("Changes saved to CSV file.");
                            break;
                        case "json":
                            AddressBookJSONFile.WriteAddressBooksToJson(addressBooks, jsonFilePath);
                            Console.WriteLine("Changes saved to JSON file.");
                            break;
                        case "all":
                            AddressBookFileIO.WriteAddressBooksToFile(addressBooks, filePath);
                            AddressBookCSVFile.WriteAddressBooksToCsv(addressBooks, csvFilePath);
                            AddressBookJSONFile.WriteAddressBooksToJson(addressBooks, jsonFilePath);
                            Console.WriteLine("Changes saved to all formats.");
                            break;
                        default:
                            Console.WriteLine("Invalid file format selected.");
                            break;
                    }
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


        // Method to delete contact from address book in different file formats
        public void DeleteContact(AddressBook addressBook, string filePath, string csvFilePath, string jsonFilePath, string selectedFormat)
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

                    switch (selectedFormat.ToLower())
                    {
                        case "txt":
                            AddressBookFileIO.WriteAddressBooksToFile(addressBooks, filePath);
                            Console.WriteLine("Changes saved to TXT file.");
                            break;
                        case "csv":
                            AddressBookCSVFile.WriteAddressBooksToCsv(addressBooks, csvFilePath);
                            Console.WriteLine("Changes saved to CSV file.");
                            break;
                        case "json":
                            AddressBookJSONFile.WriteAddressBooksToJson(addressBooks, jsonFilePath);
                            Console.WriteLine("Changes saved to JSON file.");
                            break;
                        case "all":
                            AddressBookFileIO.WriteAddressBooksToFile(addressBooks, filePath);
                            AddressBookCSVFile.WriteAddressBooksToCsv(addressBooks, csvFilePath);
                            AddressBookJSONFile.WriteAddressBooksToJson(addressBooks, jsonFilePath);
                            Console.WriteLine("Changes saved to all formats.");
                            break;
                        default:
                            Console.WriteLine("Invalid file format selected.");
                            break;
                    }
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
            Console.WriteLine("Contacts Deleted Successfully...!");
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
}
