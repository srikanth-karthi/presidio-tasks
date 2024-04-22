using MovieBooking_Library.Models;

namespace MovieBooking_Library.Repository
{
    public class MovieRepository : IRepository<string, Movie>
    {
        public readonly Dictionary<string, Movie> movies;

        public MovieRepository()
        {
            movies = new Dictionary<string, Movie>();

            Add(new Movie("Avengers", "Action", 180, new DateTime(2022, 4, 25, 15, 0, 0), 220));
            Add(new Movie("Titanic", "Romance", 195, new DateTime(2024, 8, 26, 14, 30, 0), 220));
        }

        public Movie Add(Movie movie)
        {
            if (movies.ContainsKey(movie.Title))
            {
                throw new ArgumentException($"Movie '{movie.Title}' already exists.");
            }

            movies.Add(movie.Title, movie);
            return movie;
        }

        public Movie Delete(string key)
        {
            if (!movies.ContainsKey(key))
            {
                throw new KeyNotFoundException($"Movie '{key}' not found.");
            }

            Movie movie = movies[key];
            movies.Remove(key);
            return movie;
        }

        public Movie Get(string key)
        {
            if (!movies.ContainsKey(key))
            {
                throw new KeyNotFoundException($"Movie '{key}' not found.");
            }

            return movies[key];
        }

        public void PrintAllMovies()
        {
            foreach (var kvp in movies)
            {
                Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
            }
        }

        public Dictionary<string, Movie> GetAll()
        {
            return movies;
        }

        public Movie Update(Movie movie)
        {
            if (!movies.ContainsKey(movie.Title))
            {
                throw new KeyNotFoundException($"Movie '{movie.Title}' not found.");
            }

            movies[movie.Title] = movie;
            return movie;
        }
    }
}
