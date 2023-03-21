using ProjectOverdrive.API.Data.ValueObjects.Request;
using ProjectOverdrive.API.Enum;
using ProjectOverdrive.API.Models;

namespace ProjectOverdrive.API.Data.ValueObjects.Response
{
    public class SearchCompanyResponse
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public string CompanyName { get; set; }
        public string FantasyName { get; set; }
        public string Cnae { get; set; }
        public string LegalNature { get; set; }
        public double Finance { get; set; }
        public int? AddressId { get; set; }
        public virtual AddressResponse Address { get; set; }
    }
}
