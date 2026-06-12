using Dapper;
using MostafaSaidPortfolio.Data.Repositories.Interfaces;
using Npgsql;

namespace MostafaSaidPortfolio.Data.Repositories.Implementations
{
    public abstract class BaseRepository<T> where T : class
    {
        protected readonly NpgsqlConnection _connection;
        protected NpgsqlTransaction? _transaction;

        protected BaseRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        internal void SetTransaction(NpgsqlTransaction? transaction)
        {
            _transaction = transaction;
        }

        protected abstract string TableName { get; }
        protected abstract string Columns { get; }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _connection.QueryFirstOrDefaultAsync<T>(
                $@"SELECT {Columns} FROM ""{TableName}"" WHERE ""Id"" = @id",
                new { id }, _transaction);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _connection.QueryAsync<T>(
                $@"SELECT {Columns} FROM ""{TableName}"" ORDER BY ""Id"" DESC",
                transaction: _transaction);
        }

        public virtual async Task<int> CountAsync()
        {
            return await _connection.ExecuteScalarAsync<int>(
                $@"SELECT COUNT(*) FROM ""{TableName}""",
                transaction: _transaction);
        }

        public abstract Task<int> AddAsync(T entity);
        public abstract Task<bool> UpdateAsync(T entity);

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var rows = await _connection.ExecuteAsync(
                $@"DELETE FROM ""{TableName}"" WHERE ""Id"" = @id",
                new { id }, _transaction);
            return rows > 0;
        }
    }
}
