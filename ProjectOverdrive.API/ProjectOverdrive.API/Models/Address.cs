using ProjectOverdrive.API.Models.Base;

namespace ProjectOverdrive.API.Models
{
    public class Address : BaseEntity
    {
        public string Cep { get; set; }
        public string Street { get; set; }
        public int District { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string Contact { get; set; }
    }
}
