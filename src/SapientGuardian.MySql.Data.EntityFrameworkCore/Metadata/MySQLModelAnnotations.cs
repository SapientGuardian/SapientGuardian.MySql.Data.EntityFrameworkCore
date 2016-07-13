using Microsoft.EntityFrameworkCore.Metadata;
using MySql.Data.EntityFramework7.Metadata;

namespace MySQL.Data.Entity.Metadata
{
	public class MySQLModelAnnotations : RelationalModelAnnotations
	{
		public MySQLModelAnnotations(IModel model)
			: base(model, MySQLFullAnnotationNames.Instance)
		{ }
	}
}
