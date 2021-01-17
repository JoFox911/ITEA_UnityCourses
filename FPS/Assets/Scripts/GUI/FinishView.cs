using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishView : MonoBehaviour
{
    [SerializeField]
    private Button _okBtn;

    void Awake()
    {
        if (_okBtn != null)
        {
            _okBtn.onClick.AddListener(GoToMenu);
        }
        else
        {
            Common.ObjectNotAssignedWarning("OkButton");
        }
       
    }

    //void OnEnable()
    //{
    //    AudioManager.PlayMusic(MusicType.Victory);
    //}

    //private void OnDisable()
    //{
    //    AudioManager.StopMusic();
    //}

    private void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
