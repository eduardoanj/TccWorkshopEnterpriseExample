using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Registration.UserRegistrationEnterpriseExample.Domain.Entidades;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Mappings;

public class UserWorkshopMapping : BaseMapping<UserWorkshop>
{
    public override string TableName => "user_workshops";
    
    protected override void MapearEntidade(EntityTypeBuilder<UserWorkshop> entityTypeBuilder)
    {
        entityTypeBuilder
            .HasKey(bc => new { bc.UserId, bc.WorkshopId });  
        
        entityTypeBuilder
            .HasOne(bc => bc.User)
            .WithMany(b => b.WorkShopsSubscribed)
            .HasForeignKey(bc => bc.UserId);
        
        entityTypeBuilder
            .HasOne(bc => bc.WorkShop)
            .WithMany(c => c.UsersSubscribed)
            .HasForeignKey(bc => bc.WorkshopId);
    }
}