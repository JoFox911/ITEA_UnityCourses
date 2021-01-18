using System;
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

        return orderedSearchingPoints[UnityEngine.Random.Range(0, nearestPointDispersion)];
    }

    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
}
