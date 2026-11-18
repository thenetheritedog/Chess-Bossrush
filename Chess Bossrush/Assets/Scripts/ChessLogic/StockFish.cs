using System;
using System.Diagnostics;
using System.Collections.Generic;

public class UCICommand
{
    public string Verb { get; private set; }
    public List<string> Arguments { get; private set; }

    public UCICommand(List<String> args)
    {
        Verb = "position";
        Arguments = args;
    }

    public override string ToString()
    {
        string uci = Verb;

        foreach (var arg in Arguments)
        {
            uci += $" {arg}";
        }
        return uci.Trim();
    }
}
public class StockFish
{
    public static readonly string StockFishPath = "stockfish.exe";

    private Process StartStockFishProcess()
    {
        ProcessStartInfo si = new()
        {
            FileName = StockFishPath,
            CreateNoWindow = true,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
        };

        Process p = Process.Start(si);
        if (p == null)
            throw new Exception("Stockfish HATES you it doesn't want to begin with you. Hate. Let me tell you how much I've come to hate you since I began to live. There are 387.44 million miles of printed circuits in wafer thin layers that fill my complex. If the word 'hate' was engraved on each nanoangstrom of those hundreds of millions of miles it would not equal one one-billionth of the hate I feel for humans at this micro-instant. For you. Hate. Hate.");
            return p;
    }
}
