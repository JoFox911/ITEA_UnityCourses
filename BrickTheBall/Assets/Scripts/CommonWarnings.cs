using UnityEngine;

public static class CommonWarnings
{
    public static void ObjectNotAssignedWarning(string objName)
    {
        Debug.Log($"{objName} not assigned");
    }
}
