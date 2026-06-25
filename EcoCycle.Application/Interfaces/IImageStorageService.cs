namespace EcoCycle.Application.Interfaces
{
    public interface IImageStorageService
    {
        Task<string> SaveImageAsync(Stream fileStream, string fileName);
        Task DeleteImageAsync(string imageUrl);
    }
}
