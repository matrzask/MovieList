using System.ComponentModel.DataAnnotations;


public class MovieDto
{
    public int? Id { get; set; }
    
    public string? Title { get; set; }
    
    public int? ReleaseYear { get; set; }
    
    public string? Genre { get; set; }
    
}