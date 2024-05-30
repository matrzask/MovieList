using System.ComponentModel;

namespace MovieList.Models;

public class ListIndexViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    [DisplayName("Release Year")]
    public int? ReleaseYear { get; set; }
    public string? Genre { get; set; }
    public string? Note { get; set; }
}