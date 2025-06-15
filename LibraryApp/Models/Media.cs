public abstract class Media
{
    public Guid Id { get; }
    public string Title { get; }
    public string Author { get; }

    public string Genre { get; } // e.g., Fiction, Non-Fiction, Action, Drama

    public DateTime ReleaseDate { get; } // e.g., publication date for books, release date for movies
    public abstract MediaType MediaType { get; } // e.g., Book, Movie, Music

    // Constructor in abstract class
    protected Media(string title, string author, DateTime releaseDate, string genre)
    {
        Id = Guid.NewGuid(); // Automatically generate a unique ID
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Author = author ?? throw new ArgumentNullException(nameof(author));
        ReleaseDate = releaseDate;
        Genre = genre ?? throw new ArgumentNullException(nameof(genre));
    }
}