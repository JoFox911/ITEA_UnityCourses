using UnityEngine;

public class UIView : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseScreen;

	[SerializeField]
	private GameObject _settinsScreen;

	[SerializeField]
	private GameObject _victoryScreen;

	[SerializeField]
	private GameObject _gameOverScreen;

	void Awake()
	{
		GameEvents.OnGameFinished += OnGameFinished;
		GameEvents.OnGameOver += OnGameOver;
	}

	void Update()
	{
		//uses the escape button to pause and unpause the game
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PauseControl();
		}
	}

	void OnDestroy()
	{
		GameEvents.OnGameFinished -= OnGameFinished;
		GameEvents.OnGameOver -= OnGameOver;
	}



	private void OnGameFinished()
	{
		if (_victoryScreen != null)
		{ 
			_victoryScreen.SetActive(true);
		}
		else
		{
			CommonWarnings.ObjectNotAssignedWarning("VictoryScreen");
		}
	}

	private void OnGameOver()
	{
		if (_victoryScreen != null)
		{
			_gameOverScreen.SetActive(true);
		}
		else
		{
			CommonWarnings.ObjectNotAssignedWarning("GameOverScreen");
		}
	}

	private void PauseControl()
	{
		if (_victoryScreen == null || _gameOverScreen == null || _settinsScreen == null)
		{
			CommonWarnings.ObjectNotAssignedWarning("GameOverScreen/GameOverScreen/SettinsScreen");
			return;
		}

		// ignore esc on victory and game over screens
		if (_victoryScreen.activeInHierarchy || _gameOverScreen.activeInHierarchy)
		{
			return;
		}
		
		if (GameManager.IsGamePaused())
		{
			// если открт скрин настроек поверх скрина паузы,
			// то первым нажатием на esc закроем настройки
			if (_settinsScreen.activeInHierarchy)
			{
				HideSettingsScreen();
			}
			else
			{
				GameEvents.GameContinueClickedEvent();
				HidePauseScreen();
			}
			
		}
		else
		{
			GameEvents.GamePauseClickedEvent();
			ShowPauseScreen();
		}
	}



	private void ShowPauseScreen()
	{
		if (_pauseScreen != null)
		{
			_pauseScreen.SetActive(true);
		}
		else
		{
			CommonWarnings.ObjectNotAssignedWarning("PauseScreen");
		}	
	}

	private void HidePauseScreen()
	{
		if (_pauseScreen != null)
		{
			_pauseScreen.SetActive(false);
		}
		else
		{
			CommonWarnings.ObjectNotAssignedWarning("PauseScreen");
		}
	}

	private void HideSettingsScreen()
	{
		if (_settinsScreen != null)
		{
			_settinsScreen.SetActive(false);
		}
		else
		{
			CommonWarnings.ObjectNotAssignedWarning("SettinsScreen");
		}
		
	}
}
