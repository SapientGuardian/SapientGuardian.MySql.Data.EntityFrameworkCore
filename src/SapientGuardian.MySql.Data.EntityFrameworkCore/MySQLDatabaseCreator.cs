// Copyright © 2015, 2016 Oracle and/or its affiliates. All rights reserved.
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

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using MySQL.Data.Entity.Migrations;
using MySql.Data.MySqlClient;

namespace MySQL.Data.Entity
{
	public class MySQLDatabaseCreator : RelationalDatabaseCreator
	{
		private readonly MySQLRelationalConnection _connection;
		private readonly IMigrationsSqlGenerator _sqlGenerator;
		private readonly IMigrationCommandExecutor _commandExecutor;
		private readonly IRawSqlCommandBuilder _commandBuilder;

		public MySQLDatabaseCreator(
			MySQLRelationalConnection cxn,
			IMigrationsModelDiffer differ,
			IMigrationsSqlGenerator generator,
			IMigrationCommandExecutor executor,
			IModel model,
			IRawSqlCommandBuilder commandBuilder)
			: base(model, cxn, differ, generator, executor)
		{
			ThrowIf.Argument.IsNull(cxn, "connection");
			ThrowIf.Argument.IsNull(differ, "modelDiffer");
			ThrowIf.Argument.IsNull(generator, "generator");
			ThrowIf.Argument.IsNull(commandBuilder, "commandBuilder");
			
			_connection = cxn;
			_sqlGenerator = generator;
			_commandExecutor = executor;
			_commandBuilder = commandBuilder;
		}

		public override void Create()
		{
			using(var workingConnection = _connection.CreateSystemConnection())
			{
				_commandExecutor.ExecuteNonQuery(GetCreateOps(), workingConnection);
				MySqlConnection.ClearPool((MySqlConnection)_connection.DbConnection);
			}
		}

		public override async Task CreateAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			using(var workingConnection = _connection.CreateSystemConnection())
			{
				await _commandExecutor.ExecuteNonQueryAsync(GetCreateOps(), workingConnection, cancellationToken);
				MySqlConnection.ClearPool((MySqlConnection)_connection.DbConnection);
			}
		}

		public override void Delete()
		{
			MySqlConnection.ClearAllPools();
			using(var workingConnecton = _connection.CreateSystemConnection())
			{
				_commandExecutor.ExecuteNonQuery(GetDropOps(), workingConnecton);
			}
		}

		public override async Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			MySqlConnection.ClearAllPools();
			using(var workingConnecton = _connection.CreateSystemConnection())
			{
				await _commandExecutor.ExecuteNonQueryAsync(GetDropOps(), workingConnecton, cancellationToken);
			}
		}

		public override bool Exists()
		{
			try
			{
				_connection.Open();
				_connection.Close();
				return true;
			}
			catch(Exception ex)
			{
				MySqlException mex = ex as MySqlException;
				if(mex == null)
					throw;
				if(mex.Number == 1049)
					return false;
				if(mex.InnerException == null)
					throw;
				mex = mex.InnerException as MySqlException;
				if(mex == null)
					throw;
				if(mex.Number == 1049)
					return false;
				throw;
			}
		}

		public override Task<bool> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		protected override bool HasTables()
		{
			string sql = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + _connection.DbConnection.Database + "'";
			long count = (long)_commandBuilder.Build(sql).ExecuteScalar(_connection);
			return count != 0;
		}

		protected override async Task<bool> HasTablesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			string sql = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = `" + _connection.DbConnection.Database + "`";
			long count = (long)await _commandBuilder.Build(sql).ExecuteScalarAsync(_connection, cancellationToken: cancellationToken);
			return count != 0;
		}

		private IEnumerable<MigrationCommand> GetCreateOps()
		{
			var ops = new MigrationOperation[]
			{
				new MySQLCreateDatabaseOperation { Name = _connection.DbConnection.Database }
			};

			return _sqlGenerator.Generate(ops);
		}

		private IEnumerable<MigrationCommand> GetDropOps()
		{
			var ops = new MigrationOperation[]
			{
				new MySQLDropDatabaseOperation { Name = _connection.DbConnection.Database }
			};

			return _sqlGenerator.Generate(ops);
		}
	}
}