using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using CsvHelper;
using System.Globalization;
using RestAssured;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace AddressBookSystem
{
    interface IAddressBook
    {
        void AddContact(Contacts contact);
        void EditContact(string firstName, string lastName);
        void DeleteContact(string firstName, string lastName);
        void SortContactsByName();
        void SortContactsByCity();
        void SortContactsByState();
        void SortContactsByZip();
        void DisplayContacts();
    }
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

        // Override GetHashCode method for getting better performance in collections
        public override int GetHashCode()
        {
            return FirstName.ToLower().GetHashCode() ^ LastName.ToLower().GetHashCode();
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
            string csvFilePath = Path.Combine(folderPath, $"{fileName}.csv");
            string jsonFilePath = Path.Combine(folderPath, $"{fileName}.json");

            bool moreOperations = true;
            string selectedFormat = ""; // To store the user's format selection

            Console.WriteLine("Select file format to operate on:");
            Console.WriteLine("1. txt");
            Console.WriteLine("2. csv");
            Console.WriteLine("3. json");
            Console.WriteLine("4. All");
            int formatChoice = Convert.ToInt32(Console.ReadLine());

            switch (formatChoice)
            {
                case 1:
                    selectedFormat = "txt";
                    CreateOrLoadFile("txt", filePath, manager);
                    break;

                case 2:
                    selectedFormat = "csv";
                    CreateOrLoadFile("csv", csvFilePath, manager);
                    break;

                case 3:
                    selectedFormat = "json";
                    CreateOrLoadFile("json", jsonFilePath, manager);
                    break;

                case 4:
                    selectedFormat = "All";

                    // Create or load for .txt
                    CreateOrLoadFile("txt", filePath, manager);

                    // Create or load for .csv
                    CreateOrLoadFile("csv", csvFilePath, manager);

                    // Create or load for .json
                    CreateOrLoadFile("json", jsonFilePath, manager);

                    // Remove duplicate contacts across all formats
                    foreach (var addressBook in manager.addressBooks.Values)
                    {
                        addressBook.contactList = addressBook.contactList
                            .GroupBy(c => new { c.FirstName, c.LastName })
                            .Select(g => g.First())
                            .ToList();
                    }
                    break;

                default:
                    Console.WriteLine("Invalid choice, defaulting to 'All'.");
                    selectedFormat = "All";
                    goto case 4;
            }

            while (moreOperations)
            {
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
                            manager.AddContact(addressBook, filePath, csvFilePath, jsonFilePath, selectedFormat);
                        }

                        break;

                    case 3:
                        manager.DisplayAllAddressBooks();
                        Console.Write("Enter the name of the Address Book you want to edit contact in: ");
                        string editBookName = Console.ReadLine();
                        AddressBook editAddressBook = manager.GetAddressBook(editBookName);
                        if (editAddressBook != null)
                        {
                            manager.EditContact(editAddressBook, filePath, csvFilePath, jsonFilePath, selectedFormat);
                        }
                        break;

                    case 4:
                        manager.DisplayAllAddressBooks();
                        Console.Write("Enter the name of the Address Book you want to delete contact from: ");
                        string deleteBookName = Console.ReadLine();
                        AddressBook deleteAddressBook = manager.GetAddressBook(deleteBookName);
                        if (deleteAddressBook != null)
                        {
                            manager.DeleteContact(deleteAddressBook, filePath, csvFilePath, jsonFilePath, selectedFormat);
                        }
                        break;

                    case 5:
                        Console.Write("Enter the city or state to search for a person: ");
                        string cityOrState = Console.ReadLine();
                        manager.SearchPersonByCityOrState(cityOrState);
                        break;

                    case 6:
                        Console.Write("Enter the city or state to count contacts: ");
                        string cityOrStateToCount = Console.ReadLine();
                        manager.CountByCityOrState(cityOrStateToCount);
                        break;

                    case 7:
                        manager.DisplayAllAddressBooks();
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
                                    break;
                                case 2:
                                    manager.SortContactsByCity(sortAddressBook);
                                    break;
                                case 3:
                                    manager.SortContactByState(sortAddressBook);
                                    break;
                                case 4:
                                    manager.SortContactsByZip(sortAddressBook);
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                                    break;
                            }

                            // Write the sorted contacts to both files, if applicable
                            if (selectedFormat == "txt" || selectedFormat == "All")
                                AddressBookFileIO.WriteAddressBooksToFile(manager.addressBooks, filePath);
                            if (selectedFormat == "csv" || selectedFormat == "All")
                                AddressBookCSVFile.WriteAddressBooksToCsv(manager.addressBooks, csvFilePath);
                            if (selectedFormat == "json" || selectedFormat == "All")
                                AddressBookJSONFile.WriteAddressBooksToJson(manager.addressBooks, jsonFilePath);
                        }
                        break;

                    case 8:
                        manager.DisplayAllAddressBooks();
                        foreach (var addressBookNames in manager.addressBooks.Values)
                        {
                            Console.WriteLine($"Contacts in Address Book '{addressBookNames.Name}':");
                            addressBookNames.DisplayContacts();
                        }
                        break;

                    case 9:
                        Console.WriteLine("Enter the File name to View (txt/csv):");
                        string file = Console.ReadLine();
                        if (file == "txt" && (selectedFormat == "txt" || selectedFormat == "All"))
                        {
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
                        }
                        else if (file == "csv" && (selectedFormat == "csv" || selectedFormat == "All"))
                        {
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
                        }
                        else if (file == "json" && (selectedFormat == "json" || selectedFormat == "All"))
                        {
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
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input or file format not chosen.");
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


        static void CreateOrLoadFile(string fileType, string filePath, AddressBookManager manager)
        {
            switch (fileType.ToLower())
            {
                case "txt":
                    if (!File.Exists(filePath))
                    {
                        // Create the .txt file if it doesn't exist
                        using (File.Create(filePath)) { }
                        Console.WriteLine($"File '{filePath}' did not exist, so a new file was created.");
                    }
                    else
                    {
                        // Load the address book from the existing .txt file
                        AddressBookFileIO.ReadAddressBooksFromFile(manager.addressBooks, filePath);
                        Console.WriteLine($"File '{filePath}' loaded successfully.");
                    }
                    break;

                case "csv":
                    if (!File.Exists(filePath))
                    {
                        // Create the .csv file if it doesn't exist
                        using (File.Create(filePath)) { }
                        Console.WriteLine($"File '{filePath}' did not exist, so a new file was created.");
                    }
                    else
                    {
                        // Load the address book from the existing .csv file
                        AddressBookCSVFile.ReadAddressBooksFromCsv(manager.addressBooks, filePath);
                        Console.WriteLine($"File '{filePath}' loaded successfully.");
                    }
                    break;

                case "json":
                    if (!File.Exists(filePath))
                    {
                        // Create the .json file if it doesn't exist
                        using (File.Create(filePath)) { }
                        Console.WriteLine($"File '{filePath}' did not exist, so a new file was created.");
                    }
                    else
                    {
                        // Load the address book from the existing .json file
                        AddressBookJSONFile.ReadAddressBooksFromJson(manager.addressBooks, filePath);
                        Console.WriteLine($"File '{filePath}' loaded successfully.");
                    }
                    break;
            }
        }

    }
}

