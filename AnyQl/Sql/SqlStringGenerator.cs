namespace AnyQl.Sql
{
    internal static class SqlStringGenerator
    {
        internal static string GenerateInsertSqlString(string[] columnNames, string[] values, string TableName)
        {
            return $"INSERT INTO {TableName}({string.Join(", ",columnNames)}) VALUES({string.Join(", ",values)})";
        }
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
