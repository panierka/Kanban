using Kanban.DataAccessLayer.Entities;
using Kanban.DataAccessLayer.Wrappers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Repositories
{
    internal static class TablesRepository
    {
        private const string TABLE_NAME = "tables";

        public static List<Table> GetTablesFromProject(int projectId)
        {
            List<Table> tables = new();

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                connection.Open();
                string query = $"select * from {TABLE_NAME} " +
                    $"join projects p on p.id = {TABLE_NAME}.master_projet_id " +
                    $"where p.id = {projectId};";
                MySqlCommand command = new(query, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var table = new Table(reader, projectId);
                    tables.Add(table);
                }
            }

            return tables;
        }

        public static void InsertTable(Table table, out bool successful)
        {
            string attributes = MySqlInsertBuilder.JoinNames("name", "description",
                "start_datetime", "master_projet_id");
            MySqlQueriesWrapper.Insert(table, attributes, TABLE_NAME, out successful);
        }

        public static void UpdateTable(Table table, out bool successful)
        {
            string dateFormat = MySqlVariableFormatter.DATE_FORMAT;
            string attributeUpdates = $"name = {MySqlVariableFormatter.Format(table.Name)}, " +
                $"description = {MySqlVariableFormatter.Format(table.Description)}, " +
                $"start_datetime = {MySqlVariableFormatter.Format(table.StartDateTime.ToString(dateFormat))}, " +
                $"master_projet_id = {MySqlVariableFormatter.Format(table.ProjectId)}";
            MySqlQueriesWrapper.Update(table, attributeUpdates, TABLE_NAME, out successful);
        }

        public static void RemoveTable(Table table, out bool successful)
        {
            var condition = $"where id = {table.Id}";
            MySqlQueriesWrapper.Remove(condition, TABLE_NAME, out successful);
        }
    }
}

