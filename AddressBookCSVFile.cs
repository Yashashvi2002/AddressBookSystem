using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookSystem
{
    public static class AddressBookCSVFile
    {
        public static void WriteAddressBooksToCsv(Dictionary<string, AddressBook> addressBooks, string csvFilePath)
        {
            using (var writer = new StreamWriter(csvFilePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteField("AddressBookName");
                csv.WriteField("FirstName");
                csv.WriteField("LastName");
                csv.WriteField("Address");
                csv.WriteField("City");
                csv.WriteField("State");
                csv.WriteField("Zip");
                csv.WriteField("PhoneNumber");
                csv.WriteField("Email");
                csv.NextRecord();

                foreach (var addressBookEntry in addressBooks)
                {
                    string addressBookName = addressBookEntry.Key;
                    var addressBook = addressBookEntry.Value;

                    foreach (var contact in addressBook.contactList)
                    {
                        csv.WriteField(addressBookName);
                        csv.WriteField(contact.FirstName);
                        csv.WriteField(contact.LastName);
                        csv.WriteField(contact.Address);
                        csv.WriteField(contact.City);
                        csv.WriteField(contact.State);
                        csv.WriteField(contact.Zip);
                        csv.WriteField(contact.PhoneNumber);
                        csv.WriteField(contact.Email);
                        csv.NextRecord();
                    }
                }
            }
        }

        // Read AddressBooks from a CSV file, loading AddressBook name and contacts
        public static void ReadAddressBooksFromCsv(Dictionary<string, AddressBook> addressBooks, string csvFilePath)
        {
            if (!File.Exists(csvFilePath))
            {
                Console.WriteLine("CSV file not found.");
                return;
            }

            using (StreamReader reader = new StreamReader(csvFilePath))
            using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // Read and skip the header
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    try
                    {
                        string addressBookName = csv.GetField<string>(0);
                        string firstName = csv.GetField<string>(1);
                        string lastName = csv.GetField<string>(2);
                        string address = csv.GetField<string>(3);
                        string city = csv.GetField<string>(4);
                        string state = csv.GetField<string>(5);
                        string zip = csv.GetField<string>(6);
                        string phoneNumber = csv.GetField<string>(7);
                        string email = csv.GetField<string>(8);

                        Contacts contact = new Contacts
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

                        if (!addressBooks.ContainsKey(addressBookName))
                        {
                            addressBooks[addressBookName] = new AddressBook(addressBookName);
                        }

                        addressBooks[addressBookName].contactList.Add(contact);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading CSV file: {ex.Message}");
                    }
                }
            }
        }



    }
}
