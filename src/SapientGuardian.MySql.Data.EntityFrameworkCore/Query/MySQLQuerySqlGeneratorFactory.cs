using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.Storage;
using MySQL.Data.Entity.Query;

namespace MySql.Data.EntityFramework7.Query
{
	public class MySQLQuerySqlGeneratorFactory : QuerySqlGeneratorFactoryBase
	{
		public MySQLQuerySqlGeneratorFactory(
			IRelationalCommandBuilderFactory commandBuilderFactory,
			ISqlGenerationHelper sqlGenerationHelper,
			IParameterNameGeneratorFactory parameterNameGeneratorFactory,
			IRelationalTypeMapper relationalTypeMapper)
			: base(commandBuilderFactory, sqlGenerationHelper, parameterNameGeneratorFactory, relationalTypeMapper)
		{ }

		public override IQuerySqlGenerator CreateDefault(SelectExpression selectExpression)
			=> new MySQLQuerySqlGenerator(
				CommandBuilderFactory,
				SqlGenerationHelper,
				ParameterNameGeneratorFactory,
				RelationalTypeMapper,
				selectExpression);
	}
}
