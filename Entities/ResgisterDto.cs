using System.ComponentModel.DataAnnotations;

namespace API.Entities;
public class ResgisterDto
{
    [Required]
    public required string username { get; set; }
    [Required]
    public required string password { get; set; }

}