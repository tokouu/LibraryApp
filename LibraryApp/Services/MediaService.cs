
public class MediaService : IMediaService
{
    private List<Media> _mediaCollection = new List<Media>
    {
        new Book("The Great Gatsby", "F. Scott Fitzgerald", new DateTime(1925, 4, 10), "Fiction",300, "9780743273565"),
        new CD("Abbey Road", "The Beatles", new DateTime(1969, 9, 26), "Rock", 47),
        new DVD("Inception", "Christopher Nolan", new DateTime(2010, 7, 16), "Sci-Fi", 148)
    };
    public Task<Media> AddMediaAsync(Media media)
    {
        if (media == null)
        {
            throw new ArgumentNullException(nameof(media), "Media cannot be null.");
        }
        if (_mediaCollection.Any(m => m.Id == media.Id))
        {
            throw new InvalidOperationException($"Media with ID {media.Id} already exists.");
        }
        _mediaCollection.Add(media);
        return Task.FromResult(media);
    }

    public Task<bool> DeleteMediaAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("ID cannot be empty.", nameof(id));
        }
        if (!_mediaCollection.Any(m => m.Id == id))
        {
            throw new KeyNotFoundException($"Media with ID {id} not found.");
        }
        var mediaToRemove = _mediaCollection.First(m => m.Id == id);
        _mediaCollection.Remove(mediaToRemove);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<Media>> GetAllMediaAsync()
    {
        return Task.FromResult(_mediaCollection.AsEnumerable());
    }

    public Task<Media?> GetMediaByIdAsync(Guid id)
    {
        Console.WriteLine($"=== DEBUG GetMediaByIdAsync ===");
        Console.WriteLine($"Searching for ID: {id}");
        Console.WriteLine($"ID type: {id.GetType()}");
        Console.WriteLine($"Collection has {_mediaCollection.Count} items");

        // Print each item and compare
        for (int i = 0; i < _mediaCollection.Count; i++)
        {
            var item = _mediaCollection[i];
            Console.WriteLine($"Item {i}: ID={item.Id}, Title={item.Title}");
            Console.WriteLine($"  ID type: {item.Id.GetType()}");
            Console.WriteLine($"  Equals check: {item.Id.Equals(id)}");
            Console.WriteLine($"  == check: {item.Id == id}");
        }

        if (id == Guid.Empty)
        {
            throw new ArgumentException("ID cannot be empty.", nameof(id));
        }

        var media = _mediaCollection.FirstOrDefault(m => m.Id == id);
        Console.WriteLine($"LINQ result: {(media != null ? media.Title : "NOT FOUND")}");

        return Task.FromResult(media);
    }

    public Task<IEnumerable<Media>> GetMediaByTypeAsync(MediaType mediaType)
    {
        if (!Enum.IsDefined(typeof(MediaType), mediaType))
        {
            throw new ArgumentOutOfRangeException(nameof(mediaType), "Invalid media type specified.");
        }
        var mediaByType = _mediaCollection.Where(m => m.MediaType == mediaType);
        return Task.FromResult(mediaByType.AsEnumerable());
    }

    public Task<IEnumerable<Media>> SearchMediaAsync(string searchTerm, MediaType? mediaType = null)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            throw new ArgumentException("Search term cannot be null or empty.", nameof(searchTerm));
        }

        var searchResults = _mediaCollection.Where(m =>
            m.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            m.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            m.Genre.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            m.Id.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase)); // Added ID search

        if (mediaType.HasValue)
        {
            searchResults = searchResults.Where(m => m.MediaType == mediaType.Value);
        }

        return Task.FromResult(searchResults.AsEnumerable());
    }
}