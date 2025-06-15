public interface IMediaService
{
    Task<IEnumerable<Media>> GetAllMediaAsync();
    Task<Media?> GetMediaByIdAsync(Guid id);
    Task<Media> AddMediaAsync(Media media);
    Task<bool> DeleteMediaAsync(Guid id);
    Task<IEnumerable<Media>> SearchMediaAsync(string searchTerm, MediaType? mediaType = null);
    Task<IEnumerable<Media>> GetMediaByTypeAsync(MediaType mediaType);
}