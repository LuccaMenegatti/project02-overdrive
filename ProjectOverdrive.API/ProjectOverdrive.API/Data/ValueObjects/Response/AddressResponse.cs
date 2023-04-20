using System.ComponentModel.DataAnnotations;

namespace ProjectOverdrive.API.Data.ValueObjects.Response
{
    public class AddressResponse
    {
        public string Cep { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string Contact { get; set; }
    }
}
