
# Disney Movies Watchlist

## Introduction
Welcome to the Disney Movies Watchlist web application. This project allows users to explore a collection of Disney movies, create watchlists, and manage their favorite movies. Basic users can view movies, but to create or manage watchlists, users need to sign up or log in. Authentication can be done using either local authentication (email and password) or Google Authentication.

## Features
- **View Movies**: All users can view the list of Disney movies.
- **User Authentication**: Users can sign up and log in using local authentication (email and password) or Google Authentication.
- **Watchlist Management**: Authenticated users can add movies to their watchlist, remove movies from their watchlist, and delete movies from the database.

## Repository Structure
The repository follows a clean architecture with separate layers for the data, services, and user interface. The key components are:

### Interfaces
#### IMovieRepository.cs
```csharp
namespace DisneyMoviesWatchlist.Src.Repository
{
    public interface IMovieRepository
    {
        List<Movie> GetAll(string query);
        Movie GetOne(int MovieId);
        List<MovieDto> GetWatchList(string UserId);
        void AddToWatchList(string UserId, int MovieId);
        void RemoveFromWatchList(string UserId, int MovieId);
        void DeleteFromDatabase(int MovieId);
        void DeleteFromDatabase(string UserId, int MovieId);
        bool IsInWatchList(string UserId, int MovieId);
    }
}
```

### Models
- **Movie.cs**: Represents a movie entity.
- **MovieDto.cs**: Represents a data transfer object for movies.

### Services
- **MovieService.cs**: Implements business logic for managing movies and watchlists.

### Controllers
- **MovieController.cs**: Handles HTTP requests related to movies and watchlists.

## Setup Instructions

### Prerequisites
- .NET Core SDK 3.1 or later
- Visual Studio 2019 or later / Visual Studio Code
- Azure account (for deployment)
- GitHub account

### Getting Started

1. **Clone the Repository**
   ```bash
   git clone https://github.com/your-username/DisneyMoviesWatchlist.git
   cd DisneyMoviesWatchlist
   ```

2. **Configure the Application**
   - Create an `appsettings.json` file in the root directory with the following content:
     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "YourDatabaseConnectionString"
       },
       "GoogleAuth": {
         "ClientId": "YourGoogleClientId",
         "ClientSecret": "YourGoogleClientSecret"
       }
     }
     ```
   - Replace `YourDatabaseConnectionString`, `YourGoogleClientId`, and `YourGoogleClientSecret` with your actual values.

3. **Build and Run the Application**
   - Open the project in Visual Studio and press `F5` to build and run the application.
   - Alternatively, use the .NET CLI:
     ```bash
     dotnet build
     dotnet run
     ```

### Deployment on Azure
1. **Create an Azure Web App**
   - Log in to the [Azure portal](https://portal.azure.com/).
   - Create a new Web App resource.

2. **Deploy the Application**
   - In the Azure portal, navigate to your Web App.
   - In the "Deployment Center", select GitHub as the source and authenticate.
   - Choose the repository and branch to deploy from.

3. **Configure App Settings**
   - In the Azure portal, navigate to your Web App's "Configuration" settings.
   - Add the following application settings:
     - `ConnectionStrings__DefaultConnection`: YourDatabaseConnectionString
     - `GoogleAuth__ClientId`: YourGoogleClientId
     - `GoogleAuth__ClientSecret`: YourGoogleClientSecret

4. **Access the Deployed Application**
   - Your application will be available at [https://disneyanimationmovies.azurewebsites.net/](https://disneyanimationmovies.azurewebsites.net/).

## Usage

### Viewing Movies
- Open the web application in your browser: [Disney Movies Watchlist](https://disneyanimationmovies.azurewebsites.net/).
- Browse the list of Disney movies.

### Creating an Account
- Click on "Sign Up" to create a new account using your email and password or sign up using Google Authentication.

### Managing Watchlists
- After logging in, you can add movies to your watchlist, remove movies, and delete movies from the database.

## Contributing
We welcome contributions to improve this project! Here are some ways you can contribute:
- **Report Bugs**: Use the GitHub issues to report any bugs you find.
- **Feature Requests**: Suggest new features using the GitHub issues.
- **Pull Requests**: Fork the repository and create a pull request with your changes.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more information.

## Contact
For any questions or support, please contact [your-email@example.com](mailto:your-email@example.com).

## Acknowledgements
- [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-5.0)
- [Azure](https://azure.microsoft.com/)
- [Google Authentication](https://developers.google.com/identity)

---

Thank you for visiting and using the Disney Movies Watchlist web application!
```

Make sure to replace placeholder values such as `your-username`, `your-email@example.com`, and the configuration details with your actual values specific to your project.
