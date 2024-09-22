using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookSystem
{
    public static class AddressBookJSONServer
    {
        private static readonly string apiUrl = "http://your-json-server-url/api/addressbooks";

        // Method to send (POST) address book data to the JSON server
        public static async Task WriteAddressBooksToJson(Dictionary<string, AddressBook> addressBooks)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(addressBooks, Formatting.Indented);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Address book data successfully sent to the server.");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to send data. Server returned status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending data to the server: {ex.Message}");
            }
        }

        // Method to retrieve (GET) address book data from the JSON server
        public static async Task ReadAddressBooksFromJson(Dictionary<string, AddressBook> addressBooks)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var deserializedAddressBooks = JsonConvert.DeserializeObject<Dictionary<string, AddressBook>>(json);

                        if (deserializedAddressBooks != null)
                        {
                            addressBooks.Clear();
                            foreach (var entry in deserializedAddressBooks)
                            {
                                addressBooks[entry.Key] = entry.Value;
                            }

                            Console.WriteLine("Address book data successfully retrieved from the server.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to deserialize the server response.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Server returned status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving data from the server: {ex.Message}");
            }
        }
    }
}
