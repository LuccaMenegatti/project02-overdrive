using ProjectOverdrive.API.Enum;
using ProjectOverdrive.API.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectOverdrive.API.Models 
{
    [Table("people")]
    public class People : BaseEntity
    { 
        [Column("name")]
        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        [Column("cpf")]
        [Required]
        [StringLength(14)]
        public string Cpf { get; set; }

        [Column("number_contact")]
        [StringLength(20)]
        public string NumberContact { get; set; }

        [Column("username")]
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Column("status")]
        public Status Status { get; set; }

        [ForeignKey("IdCompany")]
        public virtual Company Company { get; set; }
    }
}
