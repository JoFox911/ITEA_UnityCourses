using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Common
{
    public static void ObjectNotAssignedWarning(string objName)
    {
        Debug.LogWarning($"{objName} not assigned");
    }

    public static Transform SetectOneOfTheNearestPoint(List<Transform> transformList, Vector3 currentPossition, int variabilityNumber)
    {
        var orderedSearchingPoints = transformList.OrderBy(transform => Vector3.Distance(transform.position, currentPossition)).ToList();
        var nearestPointDispersion = transformList.Count < variabilityNumber ? transformList.Count : variabilityNumber;

        return orderedSearchingPoints[Random.Range(0, nearestPointDispersion)];
    }
}
