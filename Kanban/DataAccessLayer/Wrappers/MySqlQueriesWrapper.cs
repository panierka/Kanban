using Kanban.DataAccessLayer.Entities.Contracts;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kanban.DataAccessLayer.Exceptions;

namespace Kanban.DataAccessLayer.Wrappers
{
    internal static class MySqlQueriesWrapper
    {
        public static List<T> SelectAll<T>(string tableName, Func<MySqlDataReader, T> constructor)
        {
            List<T> collection = new();

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                connection.Open();
                string query = $"select * from {tableName}";
                MySqlCommand command = new(query, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    T item = constructor(reader);
                    collection.Add(item);
                }
            }

            return collection;
        }

        public static void Insert(IMySqlCompleteRecord item, string attributes, 
            string tableName, out bool successful)
        {
            successful = false;

            using var connection = DatabaseConnection.Instance.Connection;

            var query = $"insert into {tableName} {attributes} values {item.ToInsert()}";
            MySqlCommand command = new(query, connection);
            connection.Open();

            _ = command.ExecuteNonQuery();
            successful = true;
            item.Id = (int)command.LastInsertedId;
        }

        public static void Update(IMySqlIdentifiable item, string attributes, string tableName, 
            out bool successful)
        {
            if (item.Id is null)
            {
                throw new NoIdException(item);
            }

            string whereCondition = $"where id={item.Id}";
            successful = false;

            if (!CheckIfRecordExists(whereCondition, tableName))
            {
                throw new DatabaseNoRecordException(whereCondition, tableName);
            }

            using var connection = DatabaseConnection.Instance.Connection;

            var query = $"update {tableName} set {attributes} {whereCondition}";
            MySqlCommand command = new(query, connection);
            connection.Open();
            var rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 1)
            {
                successful = true;
            }
        }

        public static void Remove(string mySqlCondition, string tableName,
            out bool successful)
        {
            successful = false;

            if (!CheckIfRecordExists(mySqlCondition, tableName))
            {
                throw new DatabaseNoRecordException(mySqlCondition, tableName);
            }

            using var connection = DatabaseConnection.Instance.Connection;

            var query = $"delete from {tableName} where {mySqlCondition}";
            MySqlCommand command = new(query, connection);
            connection.Open();
            var rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 1)
            {
                successful = true;
            }
        }

        public static bool CheckIfRecordExists(string mySqlCondition, string tableName)
        {
            using var connection = DatabaseConnection.Instance.Connection;

            var query = $"select count(*) from {tableName} where {mySqlCondition}";
            MySqlCommand command = new(query, connection);
            connection.Open();
            int count = (int)command.ExecuteScalar();

            return count >= 1;
        }
    }
}
