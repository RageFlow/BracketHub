using System.ComponentModel.DataAnnotations;

namespace BracketHubShared.CRUD
{
    public class MemberCreateUpdateModel
    {
        public int? Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(2)]
        public string? Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(2)]
        public string? Nickname { get; set; }
    }
}
