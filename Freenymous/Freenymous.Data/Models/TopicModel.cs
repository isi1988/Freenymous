namespace Freenymous.Data.Models;

public class TopicModel
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public DateTime? CreationDate { get; set; }
    
    public List<string>? Tags { get; set; }
    
    public long UserId { get; set; }
    
    public long CommentCount { get; set; }
    
    public long CommentTopLevelCount { get; set; }
    
    public long ViewCount { get; set; }
}