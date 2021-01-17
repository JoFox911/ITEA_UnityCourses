using System.Collections.Generic;

public static class TeamNamesGenerator
{
    private readonly static List<string> _component1 = new List<string>(new string[] { "Crazy", "Black", "Angry", "Funny", "Bloody", "Mad", "Lovely" });
    private readonly static List<string> _component2 = new List<string>(new string[] { "Dinosaurs", "Panters", "Killers", "Hamsters", "Sharks", "Snipers", "Tanks" });

    public static string GenerateRandomName()
    {
        return _component1[UnityEngine.Random.Range(0, _component1.Count)] + " " + _component2[UnityEngine.Random.Range(0, _component2.Count)];
    }
}
