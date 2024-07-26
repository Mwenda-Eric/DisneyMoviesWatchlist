import sqlite3

def create_database(db_file):
    # Connect to SQLite database (creates a new file if not exists)
    conn = sqlite3.connect(db_file)
    cursor = conn.cursor()

    # Create AspNetRoles table
    cursor.execute('''
        CREATE TABLE IF NOT EXISTS AspNetRoles (
            Id TEXT PRIMARY KEY,
            Name TEXT,
            NormalizedName TEXT,
            ConcurrencyStamp TEXT
        )
    ''')

    # Create AspNetUsers table
    cursor.execute('''
        CREATE TABLE IF NOT EXISTS AspNetUsers (
            Id TEXT PRIMARY KEY,
            UserName TEXT,
            NormalizedUserName TEXT,
            Email TEXT,
            NormalizedEmail TEXT,
            EmailConfirmed INTEGER,
            PasswordHash TEXT,
            SecurityStamp TEXT,
            ConcurrencyStamp TEXT,
            PhoneNumber TEXT,
            PhoneNumberConfirmed INTEGER,
            TwoFactorEnabled INTEGER,
            LockoutEnd TEXT,
            LockoutEnabled INTEGER,
            AccessFailedCount INTEGER
        )
    ''')

    # Create AspNetRoleClaims table
    cursor.execute('''
        CREATE TABLE IF NOT EXISTS AspNetRoleClaims (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            RoleId TEXT,
            ClaimType TEXT,
            ClaimValue TEXT,
            FOREIGN KEY (RoleId) REFERENCES AspNetRoles (Id) ON DELETE CASCADE
        )
    ''')

    # Create AspNetUserClaims table
    cursor.execute('''
        CREATE TABLE IF NOT EXISTS AspNetUserClaims (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            UserId TEXT,
            ClaimType TEXT,
            ClaimValue TEXT,
            FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE
        )
    ''')

    # Create AspNetUserLogins table
    cursor.execute('''
        CREATE TABLE IF NOT EXISTS AspNetUserLogins (
            LoginProvider TEXT,
            ProviderKey TEXT,
            ProviderDisplayName TEXT,
            UserId TEXT,
            PRIMARY KEY (LoginProvider, ProviderKey),
            FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE
        )
    ''')

    # Create AspNetUserRoles table
    cursor.execute('''
        CREATE TABLE IF NOT EXISTS AspNetUserRoles (
            UserId TEXT,
            RoleId TEXT,
            PRIMARY KEY (UserId, RoleId),
            FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE,
            FOREIGN KEY (RoleId) REFERENCES AspNetRoles (Id) ON DELETE CASCADE
        )
    ''')

    # Create AspNetUserTokens table
    cursor.execute('''
        CREATE TABLE IF NOT EXISTS AspNetUserTokens (
            UserId TEXT,
            LoginProvider TEXT,
            Name TEXT,
            Value TEXT,
            PRIMARY KEY (UserId, LoginProvider, Name),
            FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE
        )
    ''')

    # Create disney_movies table
    cursor.execute('''
        CREATE TABLE IF NOT EXISTS disney_movies (
            MovieId INTEGER PRIMARY KEY AUTOINCREMENT,
            title TEXT NOT NULL,
            directors TEXT,
            genre TEXT,
            image TEXT,
            link TEXT,
            metascore INTEGER,
            rating REAL,
            runtime INTEGER,
            stars TEXT,
            summary TEXT,
            year INTEGER
        )
    ''')

    # Insert default data into the disney_movies table
    movies_data = [
        ('The Lion King', 'Roger Allers, Rob Minkoff', 'Animation, Adventure, Drama', 'lion_king.jpg', 'https://www.imdb.com/title/tt0110357/', 88, 8.5, 88, 'Matthew Broderick, Jeremy Irons, James Earl Jones', 'A lion cub crown prince is tricked by a treacherous uncle into thinking he caused his father\'s death and flees into exile in despair, only to later accept in adulthood his identity and his responsibilities.', 1994),
        ('Frozen', 'Chris Buck, Jennifer Lee', 'Animation, Adventure, Comedy', 'frozen.jpg', 'https://www.imdb.com/title/tt2294629/', 74, 7.4, 102, 'Kristen Bell, Idina Menzel, Jonathan Groff', 'When the newly crowned Queen Elsa accidentally uses her power to turn things into ice to curse her home in infinite winter, her sister Anna teams up with a mountain man, his playful reindeer, and a snowman to change the weather condition.', 2013),
        ('Moana', 'Ron Clements, John Musker', 'Animation, Adventure, Comedy', 'moana.jpg', 'https://www.imdb.com/title/tt3521164/', 81, 7.6, 107, 'Auli\'i Cravalho, Dwayne Johnson, Rachel House', 'In Ancient Polynesia, when a terrible curse incurred by the Demigod Maui reaches Moana\'s island, she answers the Ocean\'s call to seek out the Demigod to set things right.', 2016)
        # Add more movies as needed
    ]

    cursor.executemany('''
        INSERT INTO disney_movies (title, directors, genre, image, link, metascore, rating, runtime, stars, summary, year)
        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
    ''', movies_data)

    # Create MoviesAndUsers table
    cursor.execute('''
        CREATE TABLE IF NOT EXISTS MoviesAndUsers (
            UserId TEXT,
            MovieId INTEGER,
            PRIMARY KEY (UserId, MovieId),
            FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE,
            FOREIGN KEY (MovieId) REFERENCES disney_movies(MovieId) ON DELETE CASCADE
        )
    ''')

    # Commit changes and close connection
    conn.commit()
    conn.close()

if __name__ == '__main__':
    db_file = 'DisneyMoviesDB.db'
    create_database(db_file)
    print(f"SQLite database '{db_file}' and tables created successfully with default data.")
