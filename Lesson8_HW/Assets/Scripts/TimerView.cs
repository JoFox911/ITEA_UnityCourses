using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _timerText;

    void Awake()
    {
        GameEvents.OnChangeSecondsResidue += UpdateSecondsResidue;
    }

    void OnDestroy()
    {
        GameEvents.OnChangeSecondsResidue -= UpdateSecondsResidue;
    }

    private void UpdateSecondsResidue(int newValue)
    {
        _timerText.SetText(newValue.ToString());
    }
}
