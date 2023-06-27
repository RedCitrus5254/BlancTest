using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using WebApi.Storage.Contracts.Entities;
using WebApi.Storage.Contracts.Repositories;

namespace WebApi.Storage
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly string postgresConnectionString;

        public TodoItemRepository(
            string postgresConnectionString)
        {
            this.postgresConnectionString = postgresConnectionString;
        }

        public async Task<TodoItemEntity?> GetAsync(
            Guid id)
        {
            using var dbConnection = GetDbConnection();

            var item = await dbConnection.QueryFirstOrDefaultAsync<TodoItemEntity>(
                sql: @"
                    select * from todoItems
                    where id = :id;",
                param: new { id });

            return item;
        }

        public async Task AddOrUpdateAsync(
            TodoItemEntity entity)
        {
            using var dbConnection = GetDbConnection();

            await dbConnection.ExecuteAsync(
                sql: @"
                    insert into todoItems (id, title, isCompleted)
                    values (:id, :title, :isCompleted)
                    on conflict (id) do update
                    set title = :title,
                    isCompleted = :isCompleted;",
                param: new { id = entity.Id, title = entity.Title, isCompleted = entity.IsCompleted });
        }

        public async Task<List<TodoItemEntity>> GetAllAsync()
        {
            using var dbConnection = GetDbConnection();

            var entities = await dbConnection.QueryAsync<TodoItemEntity>(
                sql: @"select * from todoItems;");

            return entities.ToList();
        }

        private IDbConnection GetDbConnection()
        {
            return new NpgsqlConnection(postgresConnectionString);
        }
    }
}