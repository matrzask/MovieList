using System.ComponentModel.DataAnnotations;

namespace MovieList.Models;

public class Movie
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    [StringLength(160)]
    public string Title { get; set; }
    
    [Display(Name = "Release Year")]
    public int? ReleaseYear { get; set; }
    
    [StringLength(40)]
    public string? Genre { get; set; }
    
    public virtual ICollection<ListItem> ListItems { get; set; } = new List<ListItem>();
}