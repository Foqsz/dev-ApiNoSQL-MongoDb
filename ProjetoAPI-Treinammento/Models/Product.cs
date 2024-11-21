using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjetoAPI_Treinammento.Models;

public class Product
{   
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Nome")]
    public string Nome { get; set; } = string.Empty;
}
