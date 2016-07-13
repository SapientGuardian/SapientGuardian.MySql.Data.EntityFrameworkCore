using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MySql.Data.EntityFramework7.Query;
using MySQL.Data.Entity;
using MySQL.Data.Entity.Metadata;
using MySQL.Data.Entity.Migrations;
using MySQL.Data.Entity.Update;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class MySQLServiceCollectionExtensions
    {
		public static IServiceCollection AddMySQL(this IServiceCollection services)
		{
			services.AddRelational();

			services.TryAddEnumerable(ServiceDescriptor
				.Singleton<IDatabaseProvider, DatabaseProvider<MySQLDatabaseProviderServices, MySQLOptionsExtension>>());

			services.TryAdd(new ServiceCollection()
				.AddSingleton<MySQLValueGeneratorCache>()
				.AddSingleton<MySQLAnnotationProvider>()
				.AddSingleton<MySQLTypeMapper>()
				.AddSingleton<MySQLSqlGenerationHelper>()
				.AddSingleton<MySQLModelSource>()
				.AddSingleton<MySQLMigrationsAnnotationProvider>()
				.AddScoped<MySQLConventionSetBuilder>()
				.AddScoped<MySQLUpdateSqlGenerator>()
				.AddScoped<MySQLModificationCommandBatchFactory>()
				.AddScoped<MySQLDatabaseProviderServices>()
				.AddScoped<MySQLRelationalConnection>()
				.AddScoped<MySQLMigrationsSqlGenerator>()
				.AddScoped<MySQLDatabaseCreator>()
				.AddScoped<MySQLHistoryRepository>()
				.AddQuery());

			return services;
		}

		private static IServiceCollection AddQuery(this IServiceCollection serviceCollection)
			=> serviceCollection
				.AddScoped<MySQLCompositeMemberTranslator>()
				.AddScoped<MySQLCompositeMethodCallTranslator>()
				.AddScoped<MySQLQuerySqlGeneratorFactory>();
	}
}
