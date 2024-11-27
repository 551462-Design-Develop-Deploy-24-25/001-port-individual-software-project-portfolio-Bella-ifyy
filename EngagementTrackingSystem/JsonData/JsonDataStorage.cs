//jsonDataStorage.cs
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

// Utility class for JSON data storage
public class JsonDataStorage
{
    private string filePath;

    public JsonDataStorage(string filePath)
    {
        this.filePath = filePath;
    }

    public void SaveData<T>(T data)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new DateTimeCustomConverter() }
        });
        File.WriteAllText(filePath, json);
    }

    public T LoadData<T>()
    {
        if (!File.Exists(filePath))
        {
            return default;
        }

        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
        {
            Converters = { new DateTimeCustomConverter() }
        });
    }
}


// Custom DateTime Converter to include 12:00 PM instead of 00:00

public class DateTimeCustomConverter : JsonConverter<DateTime>
{
    private readonly string[] formats = new[]
    {
        "yyyy-MM-dd hh:mm tt",    // Handles "2024-12-17 12:00 PM"
        "yyyy-MM-ddTHH:mm:ss",    // ISO 8601
        "yyyy-MM-ddTHH:mm:ss.fffZ" // ISO 8601 with milliseconds
    };

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateString = reader.GetString();
        foreach (var format in formats)
        {
            if (DateTime.TryParseExact(dateString, format, null, System.Globalization.DateTimeStyles.None, out var date))
            {
                return date;
            }
        }

        throw new JsonException($"Unable to parse DateTime: {dateString}. Ensure it matches one of the expected formats.");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        // Serialize as "yyyy-MM-dd hh:mm tt"
        writer.WriteStringValue(value.ToString("yyyy-MM-dd hh:mm tt"));
    }
}
