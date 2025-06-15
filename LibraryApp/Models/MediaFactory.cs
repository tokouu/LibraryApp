using System.Text.Json;

namespace LibraryApp.Models
{
    public static class MediaFactory
    {
        public static Media CreateFromJson(JsonElement json)
        {
            if (!json.TryGetProperty("mediaType", out JsonElement mediaTypeElement))
            {
                throw new ArgumentException("MediaType is required");
            }

            var mediaType = mediaTypeElement.GetInt32();

            return mediaType switch
            {
                0 => CreateBook(json),
                1 => CreateCD(json),
                2 => CreateDVD(json),
                _ => throw new ArgumentException($"Invalid media type: {mediaType}")
            };
        }

        private static Book CreateBook(JsonElement json)
        {
            return new Book(
                GetRequiredString(json, "title"),
                GetRequiredString(json, "author"),
                GetRequiredDateTime(json, "releaseDate"),
                GetRequiredString(json, "genre"),
                GetOptionalInt(json, "pages"),
                GetOptionalString(json, "isbn")
            );
        }

        private static CD CreateCD(JsonElement json)
        {
            return new CD(
                GetRequiredString(json, "title"),
                GetRequiredString(json, "author"),
                GetRequiredDateTime(json, "releaseDate"),
                GetRequiredString(json, "genre"),
                GetOptionalInt(json, "durationMinutes")
            );
        }

        private static DVD CreateDVD(JsonElement json)
        {
            return new DVD(
                GetRequiredString(json, "title"),
                GetRequiredString(json, "author"),
                GetRequiredDateTime(json, "releaseDate"),
                GetRequiredString(json, "genre"),
                GetOptionalInt(json, "durationMinutes")
            );
        }

        // Helper methods for safe JSON property extraction
        private static string GetRequiredString(JsonElement json, string propertyName)
        {
            if (!json.TryGetProperty(propertyName, out JsonElement element))
            {
                throw new ArgumentException($"Required property '{propertyName}' is missing");
            }

            var value = element.GetString();
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"Property '{propertyName}' cannot be empty");
            }

            return value;
        }

        private static string GetOptionalString(JsonElement json, string propertyName)
        {
            if (json.TryGetProperty(propertyName, out JsonElement element))
            {
                return element.GetString() ?? string.Empty;
            }
            return string.Empty;
        }

        private static int GetOptionalInt(JsonElement json, string propertyName)
        {
            if (json.TryGetProperty(propertyName, out JsonElement element))
            {
                return element.GetInt32();
            }
            return 0;
        }

        private static DateTime GetRequiredDateTime(JsonElement json, string propertyName)
        {
            if (!json.TryGetProperty(propertyName, out JsonElement element))
            {
                throw new ArgumentException($"Required property '{propertyName}' is missing");
            }

            var dateString = element.GetString();
            if (string.IsNullOrWhiteSpace(dateString))
            {
                throw new ArgumentException($"Property '{propertyName}' cannot be empty");
            }

            if (!DateTime.TryParse(dateString, out DateTime result))
            {
                throw new ArgumentException($"Property '{propertyName}' is not a valid date");
            }

            return result;
        }
    }
}