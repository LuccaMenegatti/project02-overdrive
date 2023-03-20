using ProjectOverdrive.API.Enum;
using ProjectOverdrive.API.Models;

namespace ProjectOverdrive.API.Data.ValueObjects.Response
{
    public class SearchCompanyInPeopleResponse
    {
        public string Cnpj { get; set; }
        public Status Status { get; set; }
        public string CompanyName { get; set; }
        public string FantasyName { get; set; }
        public string Cnae { get; set; }
        public string LegalNature { get; set; }
    }
}
