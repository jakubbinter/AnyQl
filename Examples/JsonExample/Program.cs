using AnyQl;

//sample json string that could be response from an api with cooking recepies
//please do not use this as real cooking recepie, as I just made it up and it probably won't really work
string Json = @"{
    'count':1,
    'items':[
        {
            'recepie_name':'potatoe chips',
            'ingredients':['potatoes','salt'],
            'cooking_process':'wash your potatoes, cut them to small slices, put bit of salt on them and put them to oven for 30 minutes at 200°C'
        }
    ]
}";
//sample insert string with implicit column names
string sampleInsert = SqlGenerator.Generate(Json, new string[] { "items[0].recepie_name", "items[0].cooking_process" }, "MyRecepies", FileFormat.Json, SqlStatementType.INSERT);

//sample update string with explicit column names
string sampleUpdate = SqlGenerator.Generate(Json, new string[] { "items[0].recepie_name", "items[0].cooking_process" }, new string[] { "NameOfRecepie", "process" }, "MyRecepies", FileFormat.Json, SqlStatementType.UPDATE);

Console.WriteLine(sampleInsert);
Console.WriteLine(sampleUpdate);