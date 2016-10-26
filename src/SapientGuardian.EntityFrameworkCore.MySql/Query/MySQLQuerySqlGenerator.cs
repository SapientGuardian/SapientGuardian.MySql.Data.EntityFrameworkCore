// Copyright © 2015, 2016, Oracle and/or its affiliates. All rights reserved.
//
// MySQL Connector/NET is licensed under the terms of the GPLv2
// <http://www.gnu.org/licenses/old-licenses/gpl-2.0.html>, like most 
// MySQL Connectors. There are special exceptions to the terms and 
// conditions of the GPLv2 as it is applied to this software, see the 
// FLOSS License Exception
// <http://www.mysql.com/about/legal/licensing/foss-exception.html>.
//
// This program is free software; you can redistribute it and/or modify 
// it under the terms of the GNU General Public License as published 
// by the Free Software Foundation; version 2 of the License.
//
// This program is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License 
// for more details.
//
// You should have received a copy of the GNU General Public License along 
// with this program; if not, write to the Free Software Foundation, Inc., 
// 51 Franklin St, Fifth Floor, Boston, MA 02110-1301  USA


using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq.Expressions;

namespace MySQL.Data.Entity.Query
{
	public class MySQLQuerySqlGenerator : DefaultQuerySqlGenerator
	{
		protected override string TypedFalseLiteral => "0";
        protected override string TypedTrueLiteral => "1";

		private MySQLTypeMapper _typeMapper;

		public MySQLQuerySqlGenerator(
			   IRelationalCommandBuilderFactory relationalCommandBuilderFactory,
			   ISqlGenerationHelper sqlGenerator,
			   IParameterNameGeneratorFactory parameterNameGeneratorFactory,
			   IRelationalTypeMapper relationalTypeMapper,
			   SelectExpression selectExpression)
				: base(relationalCommandBuilderFactory, sqlGenerator, parameterNameGeneratorFactory, relationalTypeMapper, selectExpression)
		{
			_typeMapper = relationalTypeMapper as MySQLTypeMapper;
		}


		protected override void GenerateTop(SelectExpression selectExpression)
		{
			//Nothing to do
		}

		protected override void GenerateLimitOffset( SelectExpression selectExpression)
		{
			ThrowIf.Argument.IsNull(selectExpression, "selectExpression");

			if(selectExpression.Limit != null || selectExpression.Offset != null)
			{
				Sql.AppendLine().Append("LIMIT ");

				Visit(selectExpression.Limit ?? Expression.Constant(-1));

				if(selectExpression.Offset != null)
				{
					Sql.Append(" OFFSET ");

					Visit(selectExpression.Offset);
				}
			}
		}

		public override Expression VisitExplicitCast(ExplicitCastExpression explicitCastExpression)
		{
			var typeMapping = _typeMapper.FindMappingForExplicitCast(explicitCastExpression.Type);
			if(typeMapping == null)
				throw new InvalidOperationException(RelationalStrings.UnsupportedType(explicitCastExpression.Type.Name));

			Sql.Append("CAST(");
			Visit(explicitCastExpression.Operand);
			Sql.Append(" AS ");
			Sql.Append(typeMapping.StoreType);
			Sql.Append(")");

			return explicitCastExpression;
		}

		public override Expression VisitLike(LikeExpression likeExpression)
	    {
            Visit(likeExpression.Match);
            Sql.Append(" LIKE ('%' ");
            Visit(likeExpression.Pattern);
            Sql.Append(" '%')");

	        return likeExpression;
	    }

		protected override Expression VisitBinary(BinaryExpression expression)
		{
			if(expression.NodeType == ExpressionType.Add && expression.Type == typeof(string))
			{
				Sql.Append("CONCAT(");
				Visit(expression.Left);
				Sql.Append(", ");
				Visit(expression.Right);
				Sql.Append(")");

				return expression;
			}

			return base.VisitBinary(expression);
		}
	}
}
