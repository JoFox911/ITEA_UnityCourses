using UnityEngine;

public static class CommonWarnings
{
    public static void ObjectNotAssignedWarning(string objName)
    {
        Debug.LogWarning($"{objName} not assigned");
    }
}
