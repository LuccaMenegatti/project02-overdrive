using ProjectOverdrive.API.Data.ValueObjects.Response;
using System.ComponentModel.DataAnnotations;

namespace ProjectOverdrive.API.Data.ValueObjects.Request
{
    public class CompanyUpdateRequest
    {
        [Required]
        public int Id { get; set; }

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
        //public int? AddressId { get; set; }

        [Required]
        public virtual AddressResponse? Address { get; set; }
    }
}
