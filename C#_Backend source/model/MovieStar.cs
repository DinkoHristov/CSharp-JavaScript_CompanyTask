using System;

namespace Backend.Model
{
    // MovieStar class to represent the data structure for each movie star
    // given from the input.txt file
    public class MovieStar
    {
        public DateTime DateOfBirth { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string Nationality { get; set; }
    }
}
