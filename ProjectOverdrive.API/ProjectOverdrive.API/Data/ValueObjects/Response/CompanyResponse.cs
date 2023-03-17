using ProjectOverdrive.API.Enum;
using ProjectOverdrive.API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProjectOverdrive.API.Data.ValueObjects.Request;

namespace ProjectOverdrive.API.Data.ValueObjects.Response
{
    public class CompanyResponse
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }     
        public Status Status { get; set; }
        public DateTime StartDate { get; set; }
        public string CompanyName { get; set; }
        public string FantasyName { get; set; }
        public string Cnae { get; set; }
        public string LegalNature { get; set; }
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }
        public double Finance { get; set; }
        public IEnumerable<PeopleRequest> Peoples { get; set; }
    }
}
