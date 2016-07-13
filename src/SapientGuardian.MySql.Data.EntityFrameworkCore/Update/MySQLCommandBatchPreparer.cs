using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Update.Internal;
using MySQL.Data.Entity.Query;
using System.Collections.Generic;

namespace MySQL.Data.Entity
{
	public class MySQLCommandBatchPreparer : CommandBatchPreparer
	{
		public MySQLCommandBatchPreparer(
				IModificationCommandBatchFactory modificationCommandBatchFactory,
				IParameterNameGeneratorFactory parameterNameGeneratorFactory,
				IComparer<ModificationCommand> modificationCommandComparer,
				IRelationalAnnotationProvider annotations,
				MySQLValueBufferFactoryFactory valueBufferFactoryFactory)
				: base(modificationCommandBatchFactory, parameterNameGeneratorFactory, modificationCommandComparer, annotations, valueBufferFactoryFactory)
		{ }
	}
}
