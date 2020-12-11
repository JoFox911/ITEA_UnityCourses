using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;

	[SerializeField]
	private GameObject _settinsMenu;

	[SerializeField]
	private GameObject _HUD;

	void Update()
	{
		//uses the escape button to pause and unpause the game
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PauseControl();
		}
	}

	private void PauseControl()
	{
		Debug.Log("PauseControl " + GameManager.IsGamePaused());
		// останавливаем время в игре
		if (GameManager.IsGamePaused())
		{
			GameEvents.GameContinueClickedEvent();
			HidePaused();
		}
		//  запускаем его снова
		else
		{
			GameEvents.GamePauseClickedEvent();
			ShowPaused();
		}
	}



	public void ShowPaused()
	{
		_HUD.SetActive(false);
		_pauseMenu.SetActive(true);
	}

	//hides objects with ShowOnPause tag
	public void HidePaused()
	{
		_pauseMenu.SetActive(false);
		_settinsMenu.SetActive(false);
		_HUD.SetActive(true);
	}
}
