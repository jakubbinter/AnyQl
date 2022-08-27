using AnyQl;
using AnyQl.Formats;
namespace Tests
{
    [TestClass]
    public class JsonGeneratorTests
    {
        //TODO: complex tests for json
        [TestMethod]
        public void GenerateJson_SimpleJson_SimpleSQL()
        {
            string source = @"
{
    'Name':{
        'FirstName':'John'
    }
}
";
            string actual = JsonGenerator.GenerateJson(source, new string[] { "Name.FirstName" }, new string[] { "FirstName" }, "TestTable", SqlStatementType.INSERT);
            string expected = "INSERT INTO TestTable(FirstName) VALUES('John')";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GenerateJson_SimpleJsonWithArray_SimpleSQL()
        {
            string source = @"
{
    'Names':[
        {'FirstName':'John'},
        {'FirstName':'James'}       
    ]
}
";
            string actual = JsonGenerator.GenerateJson(source, new string[] { "Names[0].FirstName" }, new string[] { "FirstName" }, "TestTable", SqlStatementType.INSERT);
            string expected = "INSERT INTO TestTable(FirstName) VALUES('John')";
            Assert.AreEqual(expected, actual);
            actual = JsonGenerator.GenerateJson(source, new string[] { "Names[1].FirstName" }, new string[] { "FirstName" }, "TestTable", SqlStatementType.INSERT);
            expected = "INSERT INTO TestTable(FirstName) VALUES('James')";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GenerateJson_SimpleJsonWithMultipleProperties_SimpleSQL()
        {
            string source = @"
{
    'Name':{
        'FirstName':'John',
        'LastName':'Baker'
    }
}
";
            string actual = JsonGenerator.GenerateJson(source, new string[] { "Name.FirstName", "Name.LastName" }, new string[] { "FirstName", "LastName" }, "TestTable", SqlStatementType.INSERT);
            string expected = "INSERT INTO TestTable(FirstName, LastName) VALUES('John', 'Baker')";
            Assert.AreEqual(expected, actual);
        }
    }
}
