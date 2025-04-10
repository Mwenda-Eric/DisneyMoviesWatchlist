using DisneyMoviesWatchlist.Src.Models;

namespace DisneyMoviesWatchlist.Src.Extensions;

public static class MovieExt
{
    public static MovieDto MovieLessDetail(this Movie movie)
    {
        return new MovieDto
        {
            MovieId = movie.MovieId,
            Title = movie.Title ?? "no title",
            Year = movie.Year ?? "no year",
            Image = movie.Image ?? "lion_king.jpeg",
            Rating = movie.Rating ?? "no rating",
            Genre = movie.Genre ?? "no genres"
        };
    }
}