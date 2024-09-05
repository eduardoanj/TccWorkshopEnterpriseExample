namespace Registration.UserRegistrationEnterpriseExample.Domain.Common;

public abstract class EntidadeAuditavel
{
    protected EntidadeAuditavel()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
    public DateTime? OriginTimestampUtc { get; set; }
    public bool Deleted { get; set; }
}