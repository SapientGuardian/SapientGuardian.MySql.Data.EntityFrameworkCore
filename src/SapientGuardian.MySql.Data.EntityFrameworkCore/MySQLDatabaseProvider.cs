using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MySQL.Data.Entity
{
	public class MySQLDatabaseProvider : DatabaseProvider<MySQLDatabaseProviderServices, MySQLOptionsExtension>
    {
    }
}
