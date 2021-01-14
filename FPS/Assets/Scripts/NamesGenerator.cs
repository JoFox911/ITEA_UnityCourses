using System.Collections.Generic;

public static class NamesGenerator
{
    private readonly static List<string> _component1 = new List<string>(new string[] { "Ge", "Me", "Ta", "Bo", "Ke", "Ra", "Ne", "Mi" });
    private readonly static List<string> _component2 = new List<string>(new string[] { "oo", "ue", "as", "to", "ra", "me", "io", "so" });
    private readonly static List<string> _component3 = new List<string>(new string[] { "se", "matt", "lace", "fo", "cake", "end" });

    public static string GenerateRandomName()
    {
        return _component1[UnityEngine.Random.Range(0, _component1.Count)] +
                _component2[UnityEngine.Random.Range(0, _component2.Count)] +
                _component3[UnityEngine.Random.Range(0, _component3.Count)];
    }
}
