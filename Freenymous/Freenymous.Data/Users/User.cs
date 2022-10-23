using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Freenymous.Data.Topics;

namespace Freenymous.Data.Users;

public class User
{
    [Key]
    public long Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Country { get; set; }
    
    public int Age { get; set; }
    
    public string Gender { get; set; }
    
    [JsonIgnore]
    public byte[] Avatar { get; set; }
    
    public bool IsBusy { get; set; }
    
    public string RefreshUpdateCode { get; set; }
    
    [JsonIgnore]
    public DateTime ActiveDateTime { get; set; }
    
    [JsonIgnore]
    public List<Topic> Topics { get; set; }
}