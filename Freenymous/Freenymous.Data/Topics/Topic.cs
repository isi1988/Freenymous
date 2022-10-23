using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Freenymous.Data.Users;

namespace Freenymous.Data.Topics;

public class Topic
{
    [Key]
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string Html { get; set; }
    
    public DateTime? CreationDate { get; set; }
    
    public string? StrTags { get; set; }
    
    public List<string>? Tags { get; set; }
    
    public long UserId { get; set; }
    
    public string? UserName { get; set; }
    
    [JsonIgnore]
    public User? User { get; set; }
    
    [JsonIgnore]
    public List<Comment> Comments { get; set; }
}