// See https://aka.ms/new-console-template for more information

using Freenymous.Data;
using Freenymous.Data.Topics;
using Freenymous.Data.Users;
using Freenymous.ProfileDownloader;
using Newtonsoft.Json;

var httpClient = new HttpClient();
var response = await httpClient.GetAsync("https://api.randomuser.me/?results=100");
var json = await response.Content.ReadAsStringAsync();
var result = JsonConvert.DeserializeObject<Root>(json);

var dbcontext = new MainDbContext();

foreach (var r in result.results)
{
    try
    {
        var user = new User()
        {
            Age = 0,
            Country = r.location.country,
            Gender = r.gender,
            Avatar = await httpClient.GetByteArrayAsync(r.picture.medium),
            FirstName = r.name.first,
            LastName = r.name.last,
            RefreshUpdateCode = "",
            Topics = new List<Topic>(),
        };
        dbcontext.Users.Add(user);
        Console.WriteLine(user.FirstName+user.LastName);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
  
}

dbcontext.SaveChanges();

var x =42;