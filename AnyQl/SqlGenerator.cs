using AnyQl.Formats;
namespace AnyQl
{
    public static class SqlGenerator
    {
        public static string? Generate(string source, string[] fields, string[] columnNames, string tableName, FileFormat format, SqlStatementType type)
        {
            switch (format)
            {
                case FileFormat.Json:return JsonGenerator.GenerateJson(source, fields, columnNames, tableName, type);
                default: return null;
            }
            
        }
        public static string? Generate(string source, string[] fields, string tableName, FileFormat format, SqlStatementType type)
        {
            string[] columnNames = new string[fields.Length];
            for (int i = 0; i < columnNames.Length; i++)
            {
                columnNames[i] = fields[i].Split('.')[^1];
                columnNames[i] = columnNames[i].IndexOf('[') != -1 ? columnNames[i].Remove(columnNames[i].IndexOf('[')) : columnNames[i];
            }
            return Generate(source, fields, columnNames, tableName, format, type);
        }
        
    }
}