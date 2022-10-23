using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Freenymous.Data.Users;

namespace Freenymous.Data.Topics;

public class Comment
{
    [Key]
    public long Id { get; set; }
    
    public string? UserName { get; set; }
    
    public long? UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
    
    public long? ParentCommentId { get; set; }
    [JsonIgnore]
    public Comment? ParentComment { get; set; }
    
    public long? TopicId { get; set; }
   
    [JsonIgnore]
    public Topic? Topic { get; set; }
    
    public string Html { get; set; }
    
    public DateTime? CreationDate { get; set; }
    
    public string? AccessCode { get; set; }
}