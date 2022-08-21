using AnyQl.Formats;
namespace AnyQl
{
    /// <summary>
    /// Main class that generates sql string from defined source
    /// </summary>
    public static class SqlGenerator
    {
        /// <summary>
        /// method that generates sql string from source, with explicit names for columns in database
        /// </summary>
        /// <param name="source">source string</param>
        /// <param name="fields">fields in the source string that will be extracted</param>
        /// <param name="columnNames">names of columns in the database</param>
        /// <param name="tableName">name of table in databse</param>
        /// <param name="format">FileFormat of the source string</param>
        /// <param name="type">type of sql string that will be generated</param>
        /// <returns>generated sql string</returns>
        public static string? Generate(string source, string[] fields, string[] columnNames, string tableName, FileFormat format, SqlStatementType type)
        {
            switch (format)
            {
                case FileFormat.Json:return JsonGenerator.GenerateJson(source, fields, columnNames, tableName, type);
                default: return null;
            }
            
        }
        /// <summary>
        /// method that generates sql string from source, names for columns in database are the same as last elements of fields in the source string
        /// </summary>
        /// <param name="source">source string</param>
        /// <param name="fields">fields in the source string that will be extracted</param>
        /// <param name="tableName">name of table in databse</param>
        /// <param name="format">FileFormat of the source string</param>
        /// <param name="type">type of sql string that will be generated</param>
        /// <returns>generated sql string</returns>
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