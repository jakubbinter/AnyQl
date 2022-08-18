using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using AnyQl.Sql;

namespace AnyQl.Formats
{
    public static class JsonGenerator
    {
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
