using System.ComponentModel;

namespace ProjectOverdrive.API.Enum
{
    public enum Status
    {
        [Description("Inativo")]
        Inactive = 1,
        [Description("Ativo")]
        Active = 2,
        [Description("Pendente")]
        Pending = 3
    }
}
