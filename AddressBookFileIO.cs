using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookSystem
{
    //Class to read and write AddressBook to a file
    public static class AddressBookFileIO
    {
        // Write AddressBook with its name to a file
        public static void WriteAddressBooksToFile(Dictionary<string, AddressBook> addressBooks, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {

                writer.WriteLine("AddressBookName,FirstName,LastName,Address,City,State,Zip,PhoneNumber,Email");

                foreach (var addressBookEntry in addressBooks)
                {
                    string addressBookName = addressBookEntry.Key;
                    var addressBook = addressBookEntry.Value;


                    foreach (var contact in addressBook.contactList)
                    {
                        writer.WriteLine($"{addressBookName},{contact.FirstName},{contact.LastName},{contact.Address},{contact.City},{contact.State},{contact.Zip},{contact.PhoneNumber},{contact.Email}");
                    }
                }
            }
        }

        public static void ReadAddressBooksFromFile(Dictionary<string, AddressBook> addressBooks, string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                // Skip the header line
                string headerLine = reader.ReadLine();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Split each line by commas
                    string[] contactData = line.Split(',');


                    if (contactData.Length == 9)
                    {
                        string bookName = contactData[0].Trim();
                        string firstName = contactData[1].Trim().Trim('"');
                        string lastName = contactData[2].Trim();
                        string address = contactData[3].Trim();
                        string city = contactData[4].Trim();
                        string state = contactData[5].Trim();
                        string zip = contactData[6].Trim();
                        string phoneNumber = contactData[7].Trim();
                        string email = contactData[8].Trim();

                        // Create a new contact
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

                        if (!addressBooks.ContainsKey(bookName))
                        {
                            addressBooks[bookName] = new AddressBook(bookName);
                        }

                        addressBooks[bookName].contactList.Add(contact);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid contact format in line: {line}");
                    }
                }
            }
        }


    }
}
