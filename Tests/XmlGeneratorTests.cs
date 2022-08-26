using AnyQl;
using AnyQl.Formats;
namespace Tests
{
    [TestClass]
    public class XmlGeneratorTests
    {
        //TODO: tests for complex XMLs
        [TestMethod]
        public void GenerateXml_SimpleXml_SimpleSQL()
        {
            string source = @"
<Names>
    <Name>
        <FirstName>John</FirstName>
    </Name>
</Names>
";
            string actual=XmlGenerator.GenerateXml(source, new string[] { "Names.Name.FirstName" }, new string[] { "FirstName" }, "TestTable", SqlStatementType.INSERT);
            string expected = "INSERT INTO TestTable(FirstName) VALUES('John')";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GenerateXml_SimpleXmlWithArray_SimpleSQL()
        {
            string source = @"
<Names>
    <Name>
        <FirstName>John</FirstName>
    </Name>
    <Name>
        <FirstName>James</FirstName>
    </Name>
</Names>
";
            string actual = XmlGenerator.GenerateXml(source, new string[] { "Names.Name[0].FirstName" }, new string[] { "FirstName" }, "TestTable", SqlStatementType.INSERT);
            string expected = "INSERT INTO TestTable(FirstName) VALUES('John')";
            Assert.AreEqual(expected, actual);
            actual = XmlGenerator.GenerateXml(source, new string[] { "Names.Name[1].FirstName" }, new string[] { "FirstName" }, "TestTable", SqlStatementType.INSERT);
            expected = "INSERT INTO TestTable(FirstName) VALUES('James')";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GenerateXml_SimpleXmlWithMultipleProperties_SimpleSQL()
        {
            string source = @"
<Names>
    <Name>
        <FirstName>John</FirstName>
        <LastName>Baker</LastName>
    </Name>
</Names>
";
            string actual = XmlGenerator.GenerateXml(source, new string[] { "Names.Name.FirstName", "Names.Name.LastName" }, new string[] { "FirstName", "LastName" }, "TestTable", SqlStatementType.INSERT);
            string expected = "INSERT INTO TestTable(FirstName, LastName) VALUES('John', 'Baker')";
            Assert.AreEqual(expected, actual);
        }
    }
}