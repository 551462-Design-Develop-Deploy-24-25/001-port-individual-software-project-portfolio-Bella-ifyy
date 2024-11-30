using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class JsonDataStorage
{
    private string filePath;

    public JsonDataStorage(string relativePath)
    {
        // Ensure the path is always relative to the project directory
        this.filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", relativePath);

        // Ensure the directory exists
        var directory = Path.GetDirectoryName(this.filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }


    // Saves data to the JSON file
    public void SaveData<T>(T data)
    {
        try
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new DateTimeCustomConverter() }
            });
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data to {filePath}: {ex.Message}");
        }
    }

    // Loads data from the JSON file
    public T LoadData<T>()
    {
        try
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]"); // Create an empty array for collections
                return Activator.CreateInstance<T>();
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                Converters = { new DateTimeCustomConverter() }
            }) ?? Activator.CreateInstance<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data from {filePath}: {ex.Message}");
            return Activator.CreateInstance<T>();
        }
    }
}

// Custom DateTime Converter
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
        writer.WriteStringValue(value.ToString("yyyy-MM-dd hh:mm tt"));
    }
}
