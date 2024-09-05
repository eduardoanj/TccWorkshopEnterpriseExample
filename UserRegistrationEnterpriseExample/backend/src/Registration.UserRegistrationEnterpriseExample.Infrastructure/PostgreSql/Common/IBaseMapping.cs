using Microsoft.EntityFrameworkCore;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;

public interface IBaseMapping
{
    string TableName { get; }
    void MapearEntidade(ModelBuilder modelBuilder);
}