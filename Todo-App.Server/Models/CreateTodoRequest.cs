using System.ComponentModel.DataAnnotations;

namespace Todo_App.Server.Models
{
    public class CreateTodoRequest
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public required string Title { get; init; }
    }
}
