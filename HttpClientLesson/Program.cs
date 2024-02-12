using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HttpClientLesson
{
    public class PeopleResult
    {
        public List<Person> Results { get; set; }
        public string Next { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
        public string Height { get; set; }
        public string Mass { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            decimal TotalWeight = 0;
            decimal TotalHeight = 0;
            decimal MedianWeight = 0;
            decimal MedianHeight = 0;
            decimal Counter = 0;

            var client = new HttpClient();
            string url = "https://swapi.dev/api/people";
            while (url != null)
            {
                var response = await client.GetFromJsonAsync<PeopleResult>(url);

                foreach (var person in response.Results)
                {
                    Counter++;
                    if (person.Mass != "unknown")
                    {
                        decimal temp = Convert.ToDecimal(person.Mass);
                        TotalWeight += temp;
                    }
                    if (person.Height != "unknown")
                    {
                        decimal temp = Convert.ToDecimal(person.Height);
                        TotalHeight += temp;
                    }
                    Console.WriteLine($"{person.Name} altezza {person.Height} peso {person.Mass}");

                }

                url = response.Next;

            }

            MedianHeight = TotalHeight / Counter;
            string HeightToString = String.Format("{0:0.##}", MedianHeight);
            MedianWeight = TotalWeight / Counter;
            string WeightToString = String.Format("{0:0.##}", MedianWeight);

            Console.WriteLine();
            Console.WriteLine($"Peso Medio: {WeightToString}");
            Console.WriteLine($"Altezza Media: {HeightToString}");


            Console.Read();
        }
    }
}