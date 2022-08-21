using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using AnyQl.Sql;

namespace AnyQl.Formats
{
    /// <summary>
    /// Class Containing Methods for generating sql strings from json
    /// </summary>
    public static class JsonGenerator
    {
        /// <summary>
        /// method that generates sql string from json
        /// </summary>
        /// <param name="source">json string</param>
        /// <param name="fields">fields in the json that should be extracted</param>
        /// <param name="columnNames">names of the columns in the databse</param>
        /// <param name="TableName">name of the table in the database</param>
        /// <param name="type">type of sql string</param>
        /// <returns>generated sql string</returns>
        public static string? GenerateJson(string source, string[] fields, string[] columnNames, string TableName, SqlStatementType type)
        {
            string[] values = GetFieldValues(source, fields);
            switch (type)
            {
                case SqlStatementType.INSERT:return SqlStringGenerator.GenerateInsertSqlString(columnNames, values, TableName);
                case SqlStatementType.UPDATE:return SqlStringGenerator.GenerateUpdateSqlString(columnNames, values, TableName);
                default: return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">json string</param>
        /// <param name="fields">fields in the json that should be extracted</param>
        /// <returns>values of the fields in the json</returns>
        /// <exception cref="ArgumentException"></exception>
        private static string[] GetFieldValues(string source, string[] fields)
        {
            string[] result=new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                string field = fields[i];
                JObject obj = JObject.Parse(source);
                JToken token = obj.SelectToken("$."+field) ?? throw new ArgumentException($"field {field} is not primitive value");
                JValue value = (token as JValue) ?? throw new ArgumentException($"field {field} is not value");
                result[i] = value.Type == JTokenType.String || value.Type == JTokenType.Date ? $"'{value.Value}'" : value.Value!.ToString()!.Replace(',','.') ?? throw new ArgumentException($"field {field} is not primitive value");
            }
            return result;
        }
    }
}
