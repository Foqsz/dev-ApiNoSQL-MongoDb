using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjetoApi_MVC.Models;

public class ProductViewModel
{ 
    public string? Id { get; set; }

    [BsonElement("Nome")]
    public string Nome { get; set; } = string.Empty;

    [BsonElement("Fabricacao")]
    public int AnoDeFabricacao { get; set; }
}
