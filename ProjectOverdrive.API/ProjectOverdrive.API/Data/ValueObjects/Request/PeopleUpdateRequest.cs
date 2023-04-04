using System.ComponentModel.DataAnnotations;

namespace ProjectOverdrive.API.Data.ValueObjects.Request
{
    public class PeopleUpdateRequest
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(11, ErrorMessage = "O tamanho maximo do Contato é 11 Caracteres")]
        [MinLength(11, ErrorMessage = "O tamanho minimo do Contato é 11 Caracteres")]
        public string NumberContact { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
