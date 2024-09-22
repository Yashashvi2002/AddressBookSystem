using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookSystem
{
    // AddressBook class to store contacts details
    public class AddressBook: IAddressBook
    {
        Validator validator = new Validator();

        // Dictionaries to store contacts by city and state
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

        // Method to get contact details from user
        public Contacts GetContactDetailsFromUser()
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            if (!Validator.ValidateName(firstName))
            {
                Console.WriteLine("Invalid First Name. First Name should start with capital letter and have minimum 3 characters.");
                return null;
            }

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            if (!Validator.ValidateName(lastName))
            {
                Console.WriteLine("Invalid Last Name. Last Name should start with capital letter and have minimum 3 characters.");
                return null;
            }

            Console.Write("Enter Address: ");
            string address = Console.ReadLine();
            if (!Validator.ValidateAddress(address))
            {
                Console.WriteLine("Invalid Address. Address should have minimum 3 characters.");
                return null;
            }

            Console.Write("Enter City: ");
            string city = Console.ReadLine();
            if (!Validator.ValidateAddress(city))
            {
                Console.WriteLine("Invalid City. City should have minimum 3 characters.");
                return null;
            }

            Console.Write("Enter State: ");
            string state = Console.ReadLine();
            if (!Validator.ValidateAddress(state))
            {
                Console.WriteLine("Invalid State. State should have minimum 3 characters.");
                return null;
            }

            Console.Write("Enter Zip: ");
            string zip = Console.ReadLine();
            if (!Validator.ValidateZip(zip))
            {
                Console.WriteLine("Invalid Zip. Zip should have 6 digits.");
                return null;
            }

            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();
            if (!Validator.ValidatePhoneNumber(phoneNumber))
            {
                Console.WriteLine("Invalid Phone Number. Phone Number should have 10 digits.");
                return null;
            }

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            if (!Validator.ValidateEmail(email))
            {
                Console.WriteLine("Invalid Email. Email should be in proper format.");
                return null;
            }

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

        // Method to edit contact details
        public void EditContact(string firstName, string lastName)
        {
            // Temporary contact for comparison between new Details and existing details
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

        // Method to delete contact
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
}
