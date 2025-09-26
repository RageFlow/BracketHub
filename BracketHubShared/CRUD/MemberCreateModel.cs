using System.ComponentModel.DataAnnotations;

namespace BracketHubShared.CRUD
{
    public class MemberCreateModel
    {
        [Required(AllowEmptyStrings = false)]
        [MinLength(2)]
        public required string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(2)]
        public required string Nickname { get; set; }
    }
}
