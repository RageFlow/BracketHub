using System.ComponentModel.DataAnnotations;

namespace BracketHubShared.CRUD
{
    public class MemberReadModel
    {
        [Required]
        public int Id { get; set; }
    }
}
