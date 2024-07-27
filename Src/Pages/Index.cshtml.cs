using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DisneyMoviesWatchlist.Src.Models;
using DisneyMoviesWatchlist.Src.Repository;
using DisneyMoviesWatchlist.Src.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DisneyMoviesWatchlist.Src.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMovieRepository movieRepo;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<IndexModel> logger;

        public List<MovieDto> Movies { get; set; }
        public List<Movie> HeroSectionItems { get; set; }

        [BindProperty(SupportsGet = true)]
        public string query { get; set; }

        public IndexModel(
            IMovieRepository movieRepo,
            UserManager<IdentityUser> userManager,
            ILogger<IndexModel> logger
        )
        {
            this.movieRepo = movieRepo;
            this.userManager = userManager;
            this.logger = logger;
        }

        public void OnGet()
        {
            HeroSectionItems = new List<Movie>();
            var AllMovies = movieRepo.GetAll(query);
            Movies = AllMovies.Select(m => m.MovieLessDetail()).ToList();

            if (!string.IsNullOrEmpty(query))
            {
                Movies = Movies.Where(s => s.Title.Contains(query, StringComparison.OrdinalIgnoreCase) || s.Year.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                if (AllMovies.Count > 0)
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 4; i++)
                    {
                        int randomNumber = rnd.Next(0, AllMovies.Count);
                        HeroSectionItems.Add(AllMovies[randomNumber]);
                    }
                }
                else
                {
                    logger.LogWarning("No movies found in the database.");
                }
            }
        }

        public IActionResult OnPostAdd(int MovieId)
        {
            var userId = userManager.GetUserId(User);
            movieRepo.AddToWatchList(userId, MovieId);
            
            return RedirectToPage();
        }

        public IActionResult OnPostRemove(int MovieId)
        {
            var userId = userManager.GetUserId(User);
            movieRepo.RemoveFromWatchList(userId, MovieId);
            //movieRepo.DeleteFromDatabase(MovieId);
            
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int MovieId)
        {
            var userId = userManager.GetUserId(User);
            movieRepo.DeleteFromDatabase(userId, MovieId);
            
            return RedirectToPage();
        }

        public bool Bookmarked(int MovieId)
        {
            var userId = userManager.GetUserId(User);
            return movieRepo.IsInWatchList(userId, MovieId);
        }
    }
}
