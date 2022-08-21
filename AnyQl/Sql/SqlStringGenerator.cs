namespace AnyQl.Sql
{
    /// <summary>
    /// Class for generating sql strings
    /// </summary>
    internal static class SqlStringGenerator
    {
        /// <summary>
        /// Method that generates INSERT statement
        /// </summary>
        /// <param name="columnNames">names of the columns in the database</param>
        /// <param name="values">values from original source</param>
        /// <param name="TableName">name of table in database</param>
        /// <returns>generated INSERT string</returns>
        internal static string GenerateInsertSqlString(string[] columnNames, string[] values, string TableName)
        {
            return $"INSERT INTO {TableName}({string.Join(", ",columnNames)}) VALUES({string.Join(", ",values)})";
        }
        /// <summary>
        /// Method that generates UPDATE statement
        /// </summary>
        /// <param name="columnNames">names of the columns in the database</param>
        /// <param name="values">values from original source</param>
        /// <param name="TableName">name of table in database</param>
        /// <returns>generated UPDATE string</returns>
        internal static string GenerateUpdateSqlString(string[] columnNames, string[] values, string TableName)
        {
            string[] coulumUpdates=new string[columnNames.Length];
            for (int i = 0; i < columnNames.Length; i++)
            {
                coulumUpdates[i] = $"{columnNames[i]}={values[i]}";
            }
            return $"UPDATE {TableName} SET {string.Join(", ", coulumUpdates)}";
        }
    }
}
