using System.ComponentModel.DataAnnotations;

namespace Server.Core.src.Entity;

public class Review : BaseEntity
{
    [Range(1.0, 5.0, ErrorMessage = "Value for {0} must be between {1} and {2}")]
    public double Rating { get; set; }
    
    [MinLength(5)]
    public string Comment { get; set; } = string.Empty;
    public User User{ get; set; }
    public Guid ProductId { get; set; }
}
