using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Identity;
using DisneyMoviesWatchlist.Src.Models;
using DisneyMoviesWatchlist.Src.Repository;

namespace DisneyMoviesWatchlist.Src.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMovieRepository _movieRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMemoryCache _memoryCache;

        public List<Movie> Movies { get; set; }

        public IndexModel(IMovieRepository movieRepository, UserManager<IdentityUser> userManager, IMemoryCache memoryCache)
        {
            _movieRepository = movieRepository;
            _userManager = userManager;
            _memoryCache = memoryCache;
        }

        public void OnGet()
        {
            Movies = _movieRepository.GetAll(_userManager.GetUserId(User)).ToList();
        }
    }
}
