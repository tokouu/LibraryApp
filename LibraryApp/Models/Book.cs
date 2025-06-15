
public class Book : Media
{
    public int Pages { get; }
    public string ISBN { get; }
    public override MediaType MediaType => MediaType.Book; // Override to specify media type
    public Book(string title, string author, DateTime releaseDate, string genre, int Pages, string ISBN) : base(title, author, releaseDate, genre)
    {
        this.Pages = Pages >= 0 ? Pages : throw new ArgumentOutOfRangeException(nameof(Pages), "Pages cannot be negative.");
        this.ISBN = ISBN ?? throw new ArgumentNullException(nameof(ISBN));
    }


}