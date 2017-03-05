using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace MySQL.Data.Entity
{
    public class MySQLConventionSetBuilder : RelationalConventionSetBuilder
    {
        public MySQLConventionSetBuilder(
            IRelationalTypeMapper typeMapper,
            ICurrentDbContext currentContext,
            IDbSetFinder setFinder)
            : base(typeMapper, currentContext, setFinder)
        {
        }

        public override ConventionSet AddConventions(ConventionSet conventionSet)
        {
            ThrowIf.Argument.IsNull(conventionSet, nameof(conventionSet));

            base.AddConventions(conventionSet);
            
            return conventionSet;
        }
    }
}
