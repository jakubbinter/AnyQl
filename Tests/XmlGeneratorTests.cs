using AnyQl;
using AnyQl.Formats;
namespace Tests
{
    [TestClass]
    public class XmlGeneratorTests
    {
        //PROBLEM: there can be multiple root nodes error if we have multiple elements under one node, this is problem also for arrays
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
            string actual = XmlGenerator.GenerateXml(source, new string[] { "Names.Name.FirstName" }, new string[] { "FirstName" }, "TestTable", SqlStatementType.INSERT);
            string expected = "INSERT INTO TestTable(FirstName) VALUES('John')";
            Assert.AreEqual(expected, actual);
        }
    }
}