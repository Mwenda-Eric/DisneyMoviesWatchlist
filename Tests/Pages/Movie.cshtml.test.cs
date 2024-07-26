using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using DisneyMoviesWatchlist.Src.Pages;
using DisneyMoviesWatchlist.Src.Models;
using DisneyMoviesWatchlist.Src.Repository;

namespace DisneyMoviesWatchlist.Tests.Pages;

public class MovieModelTest
{
    private readonly Mock<UserManager<IdentityUser>> userManagerMock;
    private readonly Mock<IMovieRepository> movieRepoMock;
    public MovieModelTest()
    {
        // Initialize mocks
        movieRepoMock = new();
        userManagerMock = new(
            Mock.Of<IUserStore<IdentityUser>>(),
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null);
    }

    [Fact]
    public void OnGet_Should_Set_DisneyMovie()
    {
        // Arrange
        Movie fakeMovie = new() { MovieId = 1 };
        movieRepoMock.Setup(r => r.GetOne(1)).Returns(fakeMovie);
        var movieModel = new MovieModel(
            movieRepoMock.Object,
            userManagerMock.Object);
        // Act
        movieModel.OnGet(1);
        // Assert
        Assert.NotNull(movieModel.DisneyMovie);
        Assert.Equal(fakeMovie.MovieId, movieModel.DisneyMovie.MovieId);
    }
}
