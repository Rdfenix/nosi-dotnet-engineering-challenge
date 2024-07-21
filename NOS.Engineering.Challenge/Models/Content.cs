using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NOS.Engineering.Challenge.Models;

public class Content
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid Id { get; }
    [BsonElement("title")]
    [JsonPropertyName("title")]
    public string Title { get; }
    [BsonElement("subtitle")]
    [JsonPropertyName("subtitle")]
    public string SubTitle { get; }
    [BsonElement("description")]
    [JsonPropertyName("description")]
    public string Description { get; }
    [BsonElement("imageUrl")]
    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; }
    [BsonElement("duration")]
    [JsonPropertyName("duration")]
    public int Duration { get; }
    [BsonElement("startTime")]
    [JsonPropertyName("startTime")]
    public DateTime StartTime { get; }
    [BsonElement("endTime")]
    [JsonPropertyName("endTime")]
    public DateTime EndTime { get; }
    [BsonElement("genreList")]
    [JsonPropertyName("genreList")]
    public IEnumerable<string> GenreList { get; }


    public Content(
        Guid id,
        string title,
        string subTitle,
        string description,
        string imageUrl,
        int duration,
        DateTime startTime,
        DateTime endTime,
        IEnumerable<string> genreList
    )
    {
        Id = id;
        Title = title;
        SubTitle = subTitle;
        Description = description;
        ImageUrl = imageUrl;
        Duration = duration;
        StartTime = startTime;
        EndTime = endTime;
        GenreList = genreList;
    }
}