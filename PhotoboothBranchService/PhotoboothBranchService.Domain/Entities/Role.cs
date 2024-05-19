namespace PhotoboothBranchService.Domain.Entities
{
    public class Role
    {
        public Guid RoleID { get; set; } = default!;
        public string RoleName { get; set; } = default!;
        public virtual List<Account> Accounts { get; set; }
    }
}
