using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySQL.Data.Entity.Metadata;

namespace MySql.Data.EntityFramework7.Metadata
{
	public class MySQLFullAnnotationNames : RelationalFullAnnotationNames
	{
		public MySQLFullAnnotationNames(string prefix)
			: base(prefix)
		{ }

		public new static MySQLFullAnnotationNames Instance { get; } = new MySQLFullAnnotationNames(MySQLAnnotationNames.Prefix);
	}
}
