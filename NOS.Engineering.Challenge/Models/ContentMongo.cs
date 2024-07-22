using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NOS.Engineering.Challenge.Models;


public class ContentMongo
{
    private int? duration;
    private DateTime? startTime;
    private DateTime? endTime;
    private List<string> genreList;

    public ContentMongo(string? title, string? subTitle, string? description, string? imageUrl, int? duration, DateTime? startTime, DateTime? endTime, List<string> genreList)
    {
        Title = title;
        SubTitle = subTitle;
        Description = description;
        ImageUrl = imageUrl;
        this.duration = duration;
        this.startTime = startTime;
        this.endTime = endTime;
        this.genreList = genreList;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id
    {
        get; set;
    }
    public string? Title { get; set; }
    public string SubTitle { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public int Duration { get; set; } = 0!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<string> GenreList { get; set; } = null!;

}