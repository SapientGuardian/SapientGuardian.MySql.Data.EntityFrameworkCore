// Copyright © 2015, Oracle and/or its affiliates. All rights reserved.
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

using Microsoft.EntityFrameworkCore.Metadata;
using MySql.Data.EntityFramework7.Metadata;

namespace MySQL.Data.Entity.Metadata
{
	public static class MySQLMetadataExtension
	{
		public static IRelationalEntityTypeAnnotations MySQL(this IEntityType entityType)
			=> new RelationalEntityTypeAnnotations(entityType, MySQLFullAnnotationNames.Instance);
		public static RelationalEntityTypeAnnotations MySQL(this IMutableEntityType entityType)
			=> (RelationalEntityTypeAnnotations)MySQL((IEntityType)entityType);

		public static IRelationalForeignKeyAnnotations MySQL(this IForeignKey foreignKey)
			=> new RelationalForeignKeyAnnotations(foreignKey, MySQLFullAnnotationNames.Instance);
		public static RelationalForeignKeyAnnotations MySQL(this IMutableForeignKey foreignKey)
		   => (RelationalForeignKeyAnnotations)MySQL((IForeignKey)foreignKey);

		public static IRelationalIndexAnnotations MySQL(this IIndex index)
			=> new RelationalIndexAnnotations(index, MySQLFullAnnotationNames.Instance);
		public static RelationalIndexAnnotations MySQL(this IMutableIndex index)
			=> (RelationalIndexAnnotations)MySQL((IIndex)index);

		public static IRelationalKeyAnnotations MySQL(this IKey key)
			=> new RelationalKeyAnnotations(key, MySQLFullAnnotationNames.Instance);
		public static RelationalKeyAnnotations MySQL(this IMutableKey key)
			=> (RelationalKeyAnnotations)MySQL((IKey)key);

		public static IRelationalModelAnnotations MySQL(this IModel model)
		   => new RelationalModelAnnotations(model, MySQLFullAnnotationNames.Instance);
		public static RelationalModelAnnotations MySQL(this IMutableModel model)
			=> (RelationalModelAnnotations)MySQL((IModel)model);

		public static IRelationalPropertyAnnotations MySQL(this IProperty property)
			=> new RelationalPropertyAnnotations(property, MySQLFullAnnotationNames.Instance);
		public static RelationalPropertyAnnotations MySQL(this IMutableProperty property)
		   => (RelationalPropertyAnnotations)MySQL((IProperty)property);
	}
}
