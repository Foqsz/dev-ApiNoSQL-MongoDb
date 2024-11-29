using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI_Treinammento.Models;

public class Product
{   
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Nome")]
    [Required]
    public string Nome { get; set; } = string.Empty;

    [BsonElement("Fabricacao")]
    [Required]
    public int AnoDeFabricacao { get; set; }
}
