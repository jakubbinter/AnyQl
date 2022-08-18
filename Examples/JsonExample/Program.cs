using AnyQl;

string Json = @"{
  'Stores': [
    'Lambton Quay',
    'Willis Street'
  ],
  'Manufacturers': [
    {
      'Name': 'Acme Co',
      'Products': [
        {
          'Name': 'Anvil',
          'Price': 50
        }
      ]
    },
    {
      'Name': 'Contoso',
      'Products': [
        {
          'Name': 'Elbow Grease',
          'Price': 99.95
        },
        {
          'Name': 'Headlight Fluid',
          'Price': 4
        }
      ]
    }
  ]
}";
Console.WriteLine(SqlGenerator.Generate(Json, new string[] { "Stores[0]", "Manufacturers[1].Products[0].Price" }, "MyTable", FileFormat.Json, SqlStatementType.INSERT));
Console.WriteLine(SqlGenerator.Generate(Json, new string[] { "Stores[0]", "Manufacturers[1].Products[0].Price" }, new string[] { "Store", "Price" }, "MyTable", FileFormat.Json, SqlStatementType.UPDATE));