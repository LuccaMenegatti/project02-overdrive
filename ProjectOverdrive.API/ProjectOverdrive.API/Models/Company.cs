using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectOverdrive.API.Models
{
    [Table("company")]
    public class Company
    {
        [Column("cnpj")]
        [Required]
        [StringLength(14)]
        public string Cnpj { get; set; }

        [Column("status")]
        public bool Status { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("name_company")]
        [Required]
        [StringLength(120)]
        public string CompanyName { get; set; }

        [Column("fantasy_name")]
        [Required]
        [StringLength(120)]
        public string FantasyName { get; set; }

        [Column("cnae")]
        [Required]
        [StringLength(7)]
        public string Cnae { get; set; }

        [Column("legal_nature")]
        [Required]
        [StringLength(50)]
        public string LegalNature { get; set; }

        [ForeignKey("IdAddress")]
        public virtual Address Address { get; set;}

        [Column("Finance")]
        public double Finance { get; set; }
    }
}
