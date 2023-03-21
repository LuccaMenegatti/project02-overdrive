using ProjectOverdrive.API.Enum;
using ProjectOverdrive.API.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectOverdrive.API.Models
{
    [Table("company")]
    public class Company : BaseEntity
    {
        [Column("cnpj")]
        [Required]
        [StringLength(14)]
        public string Cnpj { get; set; }

        [Column("status")]
        public Status Status { get; set; }

        [Column("start_date")]
        [Required]
        public DateTime? StartDate { get; set; }

        [Column("name_company")]
        [Required]
        [StringLength(120)]
        public string CompanyName { get; set; }

        [Column("fantasy_name")]
        [StringLength(120)]
        public string FantasyName { get; set; }

        [Column("cnae")]
        [Required]
        [StringLength(50)]
        public string Cnae { get; set; }

        [Column("legal_nature")]
        [StringLength(50)]
        public string LegalNature { get; set; }

        [Column("IdAddress")]
        public int? IdAddress { get; set; }

        [ForeignKey("IdAddress")]
        public Address? Address { get; set;}

        [Column("Finance")]
        [Required]
        public double Finance { get; set; }
        public IEnumerable<People> Peoples { get; set; }
    }
}
