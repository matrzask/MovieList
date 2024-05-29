using System.ComponentModel.DataAnnotations;

namespace MovieList.Models;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }
    [Display(Name = "Release Year")]
    public int? ReleaseYear { get; set; }
    public string? Genre { get; set; }
}