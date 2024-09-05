using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Registration.UserRegistrationEnterpriseExample.Domain.Common;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;

public abstract class BaseMapping<T> : IBaseMapping
    where T : EntidadeAuditavel
{
    public abstract string TableName { get; }

    public void MapearEntidade(ModelBuilder modelBuilder)
    {
        var entityBuilder = modelBuilder.Entity<T>();
        MapearBase(entityBuilder);
        MapearChavePrimaria(entityBuilder);

        MapearEntidade(entityBuilder);
    }

    public void Initialize(EntityTypeBuilder<T> builder)
    {
        MapearBase(builder);
        MapearChavePrimaria(builder);

        MapearEntidade(builder);
    }

    protected abstract void MapearEntidade(EntityTypeBuilder<T> entityTypeBuilder);

    private void MapearBase(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(TableName);

        builder.Property(x => x.Id).HasColumnName("id").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("criado_em").IsRequired();
        builder.Property(x => x.LastModifiedAt).HasColumnName("ultima_modificacao_em").IsRequired();
        builder.Property(x => x.Deleted).HasColumnName("deleted");
        builder.Property(x => x.OriginTimestampUtc).HasColumnName("origin_timestamp_utc");
    }

    protected virtual void MapearChavePrimaria(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
    }
}