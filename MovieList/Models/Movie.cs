using System.ComponentModel.DataAnnotations;

namespace MovieList.Models;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int? ReleaseYear { get; set; }
    public string? Genre { get; set; }
}