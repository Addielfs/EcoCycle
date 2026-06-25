using EcoCycle.Application.Interfaces;

namespace EcoCycle.Infrastructure.Services
{
    public class ImageStorageService : IImageStorageService
    {
        private readonly string _storagePath;
        private readonly int _maxSizeMb;
        private readonly string[] _allowedExtensions;

        public ImageStorageService(
            string storagePath,
            int maxSizeMb = 5,
            string[]? allowedExtensions = null)
        {
            _storagePath = storagePath;
            _maxSizeMb = maxSizeMb;
            _allowedExtensions = allowedExtensions ?? [".jpg", ".jpeg", ".png", ".gif", ".webp"];
            Directory.CreateDirectory(_storagePath);
        }

        public async Task<string> SaveImageAsync(Stream fileStream, string fileName)
        {
            var extension = Path.GetExtension(fileName)?.ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !_allowedExtensions.Contains(extension))
                throw new InvalidOperationException(
                    $"Extensión no permitida: {extension}. " +
                    $"Extensiones válidas: {string.Join(", ", _allowedExtensions)}");

            if (fileStream.Length > _maxSizeMb * 1024 * 1024)
                throw new InvalidOperationException(
                    $"La imagen excede el tamaño máximo de {_maxSizeMb} MB");

            var uniqueName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(_storagePath, uniqueName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await fileStream.CopyToAsync(stream);

            return $"/uploads/publicaciones/{uniqueName}";
        }

        public Task DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return Task.CompletedTask;

            var fileName = Path.GetFileName(imageUrl);
            var filePath = Path.Combine(_storagePath, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);

            return Task.CompletedTask;
        }
    }
}
