using ProjectOverdrive.API.Data.ValueObjects.Response;
using System.ComponentModel.DataAnnotations;

namespace ProjectOverdrive.API.Data.ValueObjects.Request
{
    public class CompanyRequest
    {
        [Required]
        [MaxLength(14, ErrorMessage = "O tamanho maximo do CNPJ é 14 Caracteres")]
        [MinLength(14, ErrorMessage = "O tamanho minimo do CNPJ é 14 Caracteres")]
        public string Cnpj { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string FantasyName { get; set; }

        [Required]
        [MaxLength(7, ErrorMessage = "O tamanho maximo do Cnae é 7 Caracteres")]
        [MinLength(7, ErrorMessage = "O tamanho minimo do Cnae é 7 Caracteres")]
        public string Cnae { get; set; }


        public string LegalNature { get; set; }

        [Required]
        public double Finance { get; set; }

        [Required(ErrorMessage = "É obrigatório o preenchimento do endereço da empresa")]
        public virtual AddressResponse? Address { get; set; }
    }
}
