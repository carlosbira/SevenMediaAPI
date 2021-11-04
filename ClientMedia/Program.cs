using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClientMedia.Models;

namespace ClientMedia
{
    public class Program
    {
        private static HttpClient apiClient = new HttpClient();

        static void Main(string[] args)
        {
            apiClient.BaseAddress = new Uri("https://localhost:44387/sample/");

            var task0 = TestAPI();
            var task1 = GetPeopleById(42);
            var task2 = GetNamesFromUsersByAge(23);
            var task3 = GetGenderSummary();
            Task.WhenAll(task0);

            if (task0.Result)
            {
                Console.WriteLine("API Test Passed - Searching content");
                Task.Run(async () =>
                {
                    var result = await task1;
                    Console.WriteLine(result);
                });

                Task.Run(async () =>
                {
                    var result = await task2;
                    //Console.WriteLine(result);
                });

                Task.Run(async () =>
                {
                    var result = await task3;
                });

            }


            Console.ReadLine();
        }

        private static async Task<bool> TestAPI()
        {
            var response = await apiClient.GetAsync("testAPI/");
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(content);

            return await Task.FromResult((content == "API Ok"));
        }

        private static async Task<string> GetPeopleById(int id)
        {
            var response = await apiClient.GetAsync("people/" + id.ToString());
            var content = await response.Content.ReadAsAsync<PeopleDto>();
            string resultTask;

            Console.WriteLine($"Searching User's Fullname for Id equal to {id} =>");
            if (content != null)
            {
                resultTask = string.Join(" ", content.First, content.Last);
            }
            else
            {
                resultTask = "No Person find for that ID";
            }

            return await Task.FromResult(resultTask);
        }

        private static async Task<bool> GetNamesFromUsersByAge(int age)
        {
            var response = await apiClient.GetAsync("getall");
            var content = await response.Content.ReadAsAsync<List<PeopleDto>>();

            Console.WriteLine($"Searching Users' fullname for everyone that matches the age of {age} years");

            if (content != null && content.Count() > 0)
            {
                Console.WriteLine($"Found {content.Count()} users with age equal to {age} years");
                foreach(var person in content)
                {
                    Console.WriteLine($"First name: {person.First}");
                }
            }
            else
            {
                Console.WriteLine("No Persons were find for that age");
            }

            return await Task.FromResult(true);
        }

        private static async Task<bool> GetGenderSummary()
        {
            var response = await apiClient.GetAsync("gendersummary");
            var content = await response.Content.ReadAsAsync<List<GenderSummaryDto>>();

            Console.WriteLine($"Searching Genders Summary");

            if (content != null && content.Count() > 0)
            {
                foreach (var item in content)
                {
                    Console.WriteLine($"Item => Age: {item.Age} - Male: {item.MaleCount}, Female: {item.FemaleCount}, Other: {item.OtherCount}");
                }
            }
            else
            {
                Console.WriteLine("No summary found");
            }

            return await Task.FromResult(true);

        }
    }
}
