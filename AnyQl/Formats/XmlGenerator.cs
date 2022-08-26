using AnyQl.Sql;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AnyQl.Formats
{
    /// <summary>
    /// Class Containing Methods for generating sql strings from json
    /// </summary>
    public static class XmlGenerator
    {
        /// <summary>
        /// method that generates sql string from xml
        /// </summary>
        /// <param name="source">xml string</param>
        /// <param name="fields">fields in the xml that should be extracted</param>
        /// <param name="columnNames">names of the columns in the databse</param>
        /// <param name="TableName">name of the table in the database</param>
        /// <param name="type">type of sql string</param>
        /// <returns>generated sql string</returns>
        public static string GenerateXml(string source, string[] fields, string[] columnNames, string TableName, SqlStatementType type)
        {
            string[] values = GetFieldValues(source, fields);
            switch (type)
            {
                case SqlStatementType.INSERT: return SqlStringGenerator.GenerateInsertSqlString(columnNames, values, TableName);
                case SqlStatementType.UPDATE: return SqlStringGenerator.GenerateUpdateSqlString(columnNames, values, TableName);
                default: return null!;
            }
        }
        /// <summary>
        /// methods that gets values out of xml
        /// </summary>
        /// <param name="source">xml string</param>
        /// <param name="fields">fields in the xml that should be extracted</param>
        /// <returns>values of the fields in the xml</returns>
        /// <exception cref="ArgumentException"></exception>
        private static string[] GetFieldValues(string source, string[] fields)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(source);
            
            
            string[] result = new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                List<XmlNode> CurentNodes = doc.ChildNodes.ToList();
                string[] parts=fields[i].Split('.');
                foreach (string part in parts)
                {                    
                    string[] indexed = part.Split('[');
                    if(indexed.Length == 1)
                    {
                        var nodes = CurentNodes.Where(x => x.Name == indexed[0]).ToList();// doc.SelectNodes("/" + indexed[0]);
                        if (nodes!.Count > 1)
                            throw new ArgumentException($"there are multiple nodes called {part}, but no indexer is provided");
                        if(nodes.Count == 0)
                            throw new ArgumentException($"there are no nodes called {part}");

                        result[i] = nodes[0]!.InnerXml;
                        if (!(nodes[0]!.ChildNodes.Count == 1 && nodes[0]!.ChildNodes[0]!.NodeType == XmlNodeType.Text))
                        {
                            CurentNodes.Clear();
                            CurentNodes.AddRange(nodes[0].ChildNodes.ToList());
                        }
                            //doc.LoadXml(nodes[0]!.InnerXml);
                    }  
                    else if(indexed.Length == 2)
                    {
                        var nodes = CurentNodes.Where(x => x.Name == indexed[0]).ToList();
                        string toParse = indexed[1].Remove(indexed[1].Length - 1);
                        int index = int.Parse(toParse);
                        var node=nodes![index]!;
                        result[i] = node!.InnerXml;
                        if (!(node.ChildNodes.Count == 1 && node.ChildNodes[0]!.NodeType == XmlNodeType.Text))
                        {
                            CurentNodes.Clear();
                            CurentNodes.AddRange(node.ChildNodes.ToList());
                        }
                            //doc.LoadXml(node.InnerXml);
                    }
                    else
                    {
                        throw new ArgumentException($"{part} is not valid node name");
                    }
                }
                result[i] = $"'{result[i]}'";
            }
            return result;
        }

        private static List<XmlNode> ToList(this XmlNodeList original) 
        {
            List<XmlNode> result = new List<XmlNode>();
            foreach (XmlNode node in original)
            {
                result.Add(node);
            }
            return result;
        }
    }
}
