using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace MySQL.Data.Entity
{
    public class MySQLConventionSetBuilder : IConventionSetBuilder
    {
        public ConventionSet AddConventions(ConventionSet conventionSet)
        {
            ThrowIf.Argument.IsNull(conventionSet, "conventionSet");

            return conventionSet;
        }
    }
}
