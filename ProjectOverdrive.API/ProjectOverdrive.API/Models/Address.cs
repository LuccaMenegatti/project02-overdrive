using ProjectOverdrive.API.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectOverdrive.API.Models
{
    public class Address : BaseEntity
    {
        [Column("Cep")]
        [StringLength(50)]
        public string Cep { get; set; }

        [Column("Street")]
        [StringLength(120)]
        public string Street { get; set; }

        [Column("District")]
        [StringLength(120)]
        public string District { get; set; }

        [Column("Number")]
        public int Number { get; set; }

        [Column("City")]
        [StringLength(150)]
        public string City { get; set; }

        [Column("Contact")]
        [StringLength(100)]
        public string Contact { get; set; }
    }
}
