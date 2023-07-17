using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace NoticiaCadastroAPI.Model
{
    public class PlayList
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Username { get; set; } = null!;

        [BsonElement("items")]
        [JsonPropertyName("items")]
        public List<string> MovieIds { get; set; } = null!;

    }
}
