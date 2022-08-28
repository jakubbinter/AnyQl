using AnyQl;

//sample json string that could be response from an api with cooking recepies
//please do not use this as real cooking recepie, as I just made it up and it probably won't really work
string Json = @"
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
//sample insert string with implicit column names
string sampleInsert = SqlGenerator.Generate(Json, new string[] { "Recepies.Recepie[0].Name", "Recepies.Recepie[0].CookingProcess" }, "MyRecepies", FileFormat.Xml, SqlStatementType.INSERT);

//sample update string with explicit column names
string sampleUpdate = SqlGenerator.Generate(Json, new string[] { "Recepies.Recepie[0].Name", "Recepies.Recepie[0].CookingProcess" }, new string[] { "NameOfRecepie", "process" }, "MyRecepies", FileFormat.Xml, SqlStatementType.UPDATE);

Console.WriteLine(sampleInsert);
Console.WriteLine(sampleUpdate);