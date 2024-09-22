using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AddressBookSystem
{
    // Validator class to validate contact details
    public class Validator
    {
        public static bool ValidateName(string name)
        {
            return Regex.IsMatch(name, @"^[A-Z][a-z]{2,}$");
        }
        public static bool ValidateAddress(string address)
        {
            return Regex.IsMatch(address, @"^([A-Z][a-z]+)(\s[A-Z][a-z]+)*$");
        }
        public static bool ValidateEmail(string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9]+([._+-][0-9a-zA-Z]+)*@[a-zA-Z0-9-]+.[a-z]{2,3}([.][a-z]{2})*$");
        }
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^[1-9]{1}[0-9]{9}$");
        }
        public static bool ValidateZip(string zip)
        {
            return Regex.IsMatch(zip, @"^[1-9]{1}[0-9]{5}$");
        }


    }
}
