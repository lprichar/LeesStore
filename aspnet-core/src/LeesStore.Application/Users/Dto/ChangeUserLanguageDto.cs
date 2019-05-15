using System.ComponentModel.DataAnnotations;

namespace LeesStore.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}