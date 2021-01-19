using System.Collections;
using UnityEngine;

public class GrowAndShrink : MonoBehaviour
{
    // Grow parameters
    [SerializeField]
    private float _approachSpeed = 0.02f;
    [SerializeField]
    private float _growthBound = 2f;
    [SerializeField]
    private float _shrinkBound = 0.5f;
    [SerializeField]
    private float _currentRatio = 1;


    // And something to do the manipulating
    private Coroutine _routine;
    private bool _keepGoing = true;

    // Attach the coroutine
    void Awake()
    {
        // Then start the routine
        _routine = StartCoroutine(Pulse());
    }

    IEnumerator Pulse()
    {
        // Run this indefinitely
        while (_keepGoing)
        {
            // Get bigger for a few seconds
            while (_currentRatio != _growthBound)
            {
                // Determine the new ratio to use
                _currentRatio = Mathf.MoveTowards(_currentRatio, _growthBound, _approachSpeed);

                // Update our element
                transform.localScale = Vector3.one * _currentRatio;

                yield return new WaitForEndOfFrame();
            }

            // Shrink for a few seconds
            while (_currentRatio != _shrinkBound)
            {
                // Determine the new ratio to use
                _currentRatio = Mathf.MoveTowards(_currentRatio, _shrinkBound, _approachSpeed);

                // Update our text element
                transform.localScale = Vector3.one * _currentRatio;

                yield return new WaitForEndOfFrame();
            }
        }
    }
}