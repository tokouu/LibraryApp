public class DVD : Media
{
    public int Duration { get; } // Duration in minutes
    public override MediaType MediaType => MediaType.DVD; // Override to specify media type

    public DVD(string title, string author, DateTime releaseDate, string genre, int duration)
        : base(title, author, releaseDate, genre)
    {
        Duration = duration >= 0 ? duration : throw new ArgumentOutOfRangeException(nameof(duration), "Duration cannot be negative.");
    }
}