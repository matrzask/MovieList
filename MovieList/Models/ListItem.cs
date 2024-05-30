using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MovieList.Models;

public class ListItem
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public string? IdentityUserId { get; set; }
    [StringLength(1000)]
    public string? Note { get; set; }
}