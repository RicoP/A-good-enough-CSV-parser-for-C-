using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

class CSV : Dictionary<string, List<string>> {
    public int Lines { get; private set; } = -1;

    private static void Assert(bool cond, string msg) {
        if (!cond) throw new System.Exception(msg);
    }

    public static CSV Parse(string path, char separator) {
        CSV csv = new CSV();
        //csv["question"] = [ "q1", "q2", ... ]
        //csv["category"] = [ "c1", "c2", ... ]

        string csv_text = File.ReadAllText(path, Encoding.UTF8);

        if (csv_text.IndexOfAny(new char[] { 'ä', 'ö', 'ü', 'Ä', 'Ö', 'Ü' }) == -1) {
            //sometimes Libre Office exports csv files in utf7 for whatever reason. when there are no umlauts, just read in again.
            //if there aren't any umlauts in the text file to begin with then the resulting string just stays the same.
            csv_text = File.ReadAllText(path, Encoding.UTF7);
        }

        var string_reader = new StringReader(csv_text);
        var line = string_reader.ReadLine();

        var columnHeaders = line.Split(separator).Select(s => s.ToLower()).ToList();
        int columnCount = 0;
        foreach (var column in columnHeaders) {
            csv.Add(column, new List<string>());
            ++columnCount;
        }
        csv.Lines = 0;
        for (; ; ) {
            line = string_reader.ReadLine();
            if (line == null) break;
            if (string.IsNullOrWhiteSpace(line)) continue;

            var values = line.Split(separator);
            //Less values as in column.length are allowed, but not more
            Assert(columnCount <= values.Length, "to many values");
            for (var i = 0; i < columnCount; i++) {
                var col = columnHeaders[i];
                var list = csv[col];

                if (i < values.Length) {
                    list.Add(values[i]);
                }
                else {
                    list.Add("");
                }
            }
            csv.Lines++;
        }

        return csv;
    }
}
