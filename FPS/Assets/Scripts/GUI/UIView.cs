using UnityEngine;

public class UIView : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseScreen;

    [SerializeField]
	private GameObject _settinsScreen;

    [SerializeField]
    private GameObject _finishScreen;

    void Awake()
    {
		EventAgregator.Subscribe<PauseClickedEvent>(ShowPauseScreen);
		EventAgregator.Subscribe<GameFinishedEvent>(ShowFinishScreen);
	}

    void OnDestroy()
    {
        EventAgregator.Unsubscribe<PauseClickedEvent>(ShowPauseScreen);
        EventAgregator.Unsubscribe<GameFinishedEvent>(ShowFinishScreen);
    }



    private void ShowFinishScreen(object sender, GameFinishedEvent eventData)
    {
        if (_finishScreen != null)
        {
            _finishScreen.SetActive(true);
        }
        else
        {
            Common.ObjectNotAssignedWarning("FinishScreen");
        }
    }


    private void ShowPauseScreen(object sender, PauseClickedEvent eventData)
    {
        if (_pauseScreen != null)
        {
            _pauseScreen.SetActive(true);
        }
        else
        {
            Common.ObjectNotAssignedWarning("PauseScreen");
        }
    }
}
