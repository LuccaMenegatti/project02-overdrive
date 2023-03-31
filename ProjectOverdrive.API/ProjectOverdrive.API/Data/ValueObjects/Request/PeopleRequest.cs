using System.ComponentModel.DataAnnotations;

namespace ProjectOverdrive.API.Data.ValueObjects.Request
{
    public class PeopleRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(11, ErrorMessage = "O tamanho maximo do CPF é 11 Caracteres")]
        [MinLength(11, ErrorMessage = "O tamanho minimo do CPF é 11 Caracteres")]
        public string Cpf { get; set; }

        [Required]
        [MaxLength(11, ErrorMessage = "O tamanho maximo do Contato é 11 Caracteres")]
        [MinLength(11, ErrorMessage = "O tamanho minimo do Contato é 11 Caracteres")]
        public string NumberContact { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
