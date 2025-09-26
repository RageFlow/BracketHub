using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BracketHubShared.CRUD
{
    public class MemberReadModel
    {
        [Required]
        [NotNull]
        public int? Id { get; set; }
    }
}
