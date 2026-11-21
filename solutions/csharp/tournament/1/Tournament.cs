using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class Tournament
{
    private class TeamStats
    {
        public string Name { get; set; }
        public int MP => W + D + L;
        public int W { get; set; }
        public int D { get; set; }
        public int L { get; set; }
        public int P => W * 3 + D;
    }

    public static void Tally(Stream inStream, Stream outStream)
    {
        var teams = new Dictionary<string, TeamStats>();
        using var reader = new StreamReader(inStream);
        string line;

        while ((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split(';');
            var t1 = parts[0];
            var t2 = parts[1];
            var res = parts[2];

            if (!teams.ContainsKey(t1)) teams[t1] = new TeamStats { Name = t1 };
            if (!teams.ContainsKey(t2)) teams[t2] = new TeamStats { Name = t2 };

            if (res == "win") { teams[t1].W++; teams[t2].L++; }
            else if (res == "loss") { teams[t2].W++; teams[t1].L++; }
            else if (res == "draw") { teams[t1].D++; teams[t2].D++; }
        }

        var sb = new StringBuilder();
        sb.Append("Team                           | MP |  W |  D |  L |  P");

        var sorted = teams.Values
            .OrderByDescending(t => t.P)
            .ThenBy(t => t.Name);

        foreach (var t in sorted)
        {
            sb.Append($"\n{t.Name,-30} | {t.MP,2} | {t.W,2} | {t.D,2} | {t.L,2} | {t.P,2}");
        }

        using var writer = new StreamWriter(outStream);
        writer.Write(sb.ToString());
    }
}