using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MovieList.Models;

public class ListItem
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    [StringLength(100)]
    public string IdentityUserId { get; set; }
    [StringLength(1000)]
    public string? Note { get; set; }
    
    public virtual Movie Movie { get; set; } = default!;
    public virtual IdentityUser IdentityUser { get; set; } = default!;
}