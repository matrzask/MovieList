using Microsoft.AspNetCore.Identity;

namespace MovieList.Models;

public class MovieListUser : IdentityUser
{
    public virtual ICollection<ListItem> ListItems { get; set; }
}