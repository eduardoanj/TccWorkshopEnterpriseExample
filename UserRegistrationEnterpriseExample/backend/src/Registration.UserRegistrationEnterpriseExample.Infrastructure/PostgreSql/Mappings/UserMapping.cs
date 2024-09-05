using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Registration.UserRegistrationEnterpriseExample.Domain.Entidades;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Mappings;

public class UserMapping : BaseMapping<User>
{
    public override string TableName => "users";

    protected override void MapearEntidade(EntityTypeBuilder<User> entityTypeBuilder)
    {
        entityTypeBuilder.Property(p => p.Document).HasColumnName("document").IsRequired();
        entityTypeBuilder.Property(p => p.Name).HasColumnName("name").IsRequired();
        entityTypeBuilder.Property(p => p.Password).HasColumnName("password").IsRequired();
        entityTypeBuilder.Property(p => p.Email).HasColumnName("email").IsRequired();
        entityTypeBuilder.Property(p => p.UserType).HasColumnName("user_type").IsRequired();
        
    }
}