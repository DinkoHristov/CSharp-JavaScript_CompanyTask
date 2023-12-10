using Backend.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Backend.Services
{
    public class ApplicationService
    {
        //Here you should create Menu which your Console application will show to user
        //User should be able to choose between: 1. Movie star 2. Calculate Net salary 3. Exit
        public ApplicationService()
        {

        }

        // In the Run method I display each movie star
        public void Run()
        {
            var movieStars = ReadMovieStarsFromJsonFile("input.txt");

            foreach (var movieStar in movieStars) 
            {
                DisplayMovieStar(movieStar);
            }
        }

        // Here I read and deserialize the JSON data from the input.txt file
        private List<MovieStar> ReadMovieStarsFromJsonFile(string filePath)
        {
            try
            {
                // I read the input.txt file
                var json = File.ReadAllText(filePath);
                // I deserialize the JSON file
                return JsonConvert.DeserializeObject<List<MovieStar>>(json);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading JSON file: {e.Message}");
                return new List<MovieStar>();
            }
        }

        // Display the information about each movie star
        private void DisplayMovieStar(MovieStar movieStar)
        {
            Console.WriteLine($"{movieStar.Name} {movieStar.Surname}");
            Console.WriteLine($"{movieStar.Sex}");
            Console.WriteLine($"{movieStar.Nationality}");
            Console.WriteLine($"{DateTime.Now.Year - movieStar.DateOfBirth.Year} years old");
            Console.WriteLine();
        }
    }
}
