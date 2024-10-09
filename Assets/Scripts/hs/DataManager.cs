using System.Collections.Generic;
using System.Linq;

public class Row
{
    public int A;
    public float B;
    public string C;
}

public class Data
{
    public List<Row> Rows;
}

public static class Testbed
{
    public static Data LoadCsv(string csv)
    {
        var rows = CSVSerializer.Deserialize<Row>(csv);

        return new Data()
        {
            Rows = rows.ToList()
        };
    }
}