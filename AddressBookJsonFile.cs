using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookSystem
{
    public static class AddressBookJSONFile
    {
        public static void WriteAddressBooksToJson(Dictionary<string, AddressBook> addressBooks, string jsonFilePath)
        {
            using (StreamWriter writer = new StreamWriter(jsonFilePath))
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(addressBooks, Newtonsoft.Json.Formatting.Indented);
                writer.Write(json);
            }
        }

        public static void ReadAddressBooksFromJson(Dictionary<string, AddressBook> addressBooks, string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine("JSON file not found.");
                return;
            }

            using (StreamReader reader = new StreamReader(jsonFilePath))
            {
                string json = reader.ReadToEnd();

                var deserializedAddressBooks = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, AddressBook>>(json);

                if (deserializedAddressBooks != null)
                {
                    addressBooks.Clear();
                    foreach (var entry in deserializedAddressBooks)
                    {
                        addressBooks[entry.Key] = entry.Value;
                    }
                }
                else
                {
                    Console.WriteLine("Failed to deserialize JSON.");
                }
            }
        }
    }
}
