using AnyQl;
using AnyQl.Formats;
namespace Tests
{
    [TestClass]
    public class SqlGeneratorTests
    {
        [TestMethod]
        public void Generate_SampleJson_SampleSQL()
        {
            string source = @"{
    'count':1,
    'items':[
        {
            'recepie_name':'potatoe chips',
            'ingredients':['potatoes','salt'],
            'cooking_process':'wash your potatoes, cut them to small slices, put bit of salt on them and put them to oven for 30 minutes at 200°C'
        }
    ]
}";
            string expectedInsert = SqlGenerator.Generate(source, new string[] { "items[0].recepie_name", "items[0].cooking_process" }, "MyRecepies", FileFormat.Json, SqlStatementType.INSERT);

            //sample update string with explicit column names
            string expectedUpdate = SqlGenerator.Generate(source, new string[] { "items[0].recepie_name", "items[0].cooking_process" }, new string[] { "NameOfRecepie", "process" }, "MyRecepies", FileFormat.Json, SqlStatementType.UPDATE);

            string actualInsert= "INSERT INTO MyRecepies(recepie_name, cooking_process) VALUES('potatoe chips', 'wash your potatoes, cut them to small slices, put bit of salt on them and put them to oven for 30 minutes at 200°C')";
            string actualUpdate= "UPDATE MyRecepies SET NameOfRecepie='potatoe chips', process='wash your potatoes, cut them to small slices, put bit of salt on them and put them to oven for 30 minutes at 200°C'";

            Assert.AreEqual(expectedInsert, actualInsert);
            Assert.AreEqual(expectedUpdate, actualUpdate);
        }

        [TestMethod]
        public void Generate_SampleXml_SampleSQL()
        {
            string source = @"
<Recepies>
    <Count>2</Count>
    <Recepie>
        <Name>potatoe chips</Name>
        <Ingredients>
            <Ingredient>potatoes</Ingredient>
            <Ingredient>salt</Ingredient>
        </Ingredients>
        <CookingProcess>wash your potatoes, cut them to small slices, put bit of salt on them and put them to oven for 30 minutes at 200°C</CookingProcess>
    </Recepie>
    <Recepie>
        null
    </Recepie>
</Recepies>
";
            string expectedInsert = SqlGenerator.Generate(source, new string[] { "Recepies.Recepie[0].Name", "Recepies.Recepie[0].CookingProcess" }, "MyRecepies", FileFormat.Xml, SqlStatementType.INSERT);

            //sample update string with explicit column names
            string expectedUpdate = SqlGenerator.Generate(source, new string[] { "Recepies.Recepie[0].Name", "Recepies.Recepie[0].CookingProcess" }, new string[] { "NameOfRecepie", "process" }, "MyRecepies", FileFormat.Xml, SqlStatementType.UPDATE);

            string actualInsert = "INSERT INTO MyRecepies(Name, CookingProcess) VALUES('potatoe chips', 'wash your potatoes, cut them to small slices, put bit of salt on them and put them to oven for 30 minutes at 200°C')";
            string actualUpdate = "UPDATE MyRecepies SET NameOfRecepie='potatoe chips', process='wash your potatoes, cut them to small slices, put bit of salt on them and put them to oven for 30 minutes at 200°C'";

            Assert.AreEqual(expectedInsert, actualInsert);
            Assert.AreEqual(expectedUpdate, actualUpdate);
        }
        //TODO: Benchmarking
    }
}
