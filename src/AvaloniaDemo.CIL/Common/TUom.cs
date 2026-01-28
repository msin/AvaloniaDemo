namespace AvaloniaDemo.CIL.Common;

public record UomKeyVal(string Id, string Name);

public static class UomData
{
    private const string data =
        @"MIN	min
ST	pc
G	g
KG	kg
MM	mm
M	m
M2	m2
M3	m3
RM	rm
L	l
S100	pc100
TIN	sheet
ML	ml";

    public static UomKeyVal[] Data { get; } = Load();

    public static UomKeyVal[] Load() => data
        .Split(Environment.NewLine)
        .Select(t => t.Split('\t'))
        .Select(t => new UomKeyVal(t[0], t[1]))
        .ToArray();
}