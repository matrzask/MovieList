using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MovieList.Models;

public class ListItem
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public string MovieListUserId { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }
    
    public virtual Movie Movie { get; set; }
    public virtual MovieListUser MovieListUser { get; set; }
}