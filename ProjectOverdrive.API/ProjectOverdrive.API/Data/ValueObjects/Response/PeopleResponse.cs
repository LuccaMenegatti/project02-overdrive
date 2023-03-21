using ProjectOverdrive.API.Enum;
using ProjectOverdrive.API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectOverdrive.API.Data.ValueObjects.Response
{
    public class PeopleResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string NumberContact { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public int? IdCompany { get; set; }
        public virtual SearchCompanyInPeopleResponse Company { get; set; }

    }
}
