using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Registration.UserRegistrationEnterpriseExample.Domain.Entidades;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Mappings;

public class WorkshopMapping : BaseMapping<WorkShop>
{
    public override string TableName => "workshops";

    protected override void MapearEntidade(EntityTypeBuilder<WorkShop> entityTypeBuilder)
    {
        entityTypeBuilder.Property(p => p.Description).HasColumnName("description").IsRequired();
        entityTypeBuilder.Property(p => p.Date).HasColumnName("date").IsRequired();
        entityTypeBuilder.Property(p => p.Name).HasColumnName("name").IsRequired();
        entityTypeBuilder.Property(p => p.Address).HasColumnName("Address").IsRequired();
        entityTypeBuilder.Property(p => p.Image).HasColumnName("image").IsRequired();
        entityTypeBuilder.Property(p => p.ImageCreator).HasColumnName("image_creator").IsRequired();
        entityTypeBuilder.Property(p => p.IdCreator).HasColumnName("id_creator").IsRequired();
    }
}