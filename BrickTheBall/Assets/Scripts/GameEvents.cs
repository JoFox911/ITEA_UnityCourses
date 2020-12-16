using System;

public static class GameEvents
{
    // HUD

    public static event Action<int> OnRaiseScore;

    public static event Action<int> OnRaiseHighScore;

    public static event Action<int> OnChangeLives;

    public static event Action<int> OnChangeLevel;

    // game state

    public static event Action OnNewGame;

    public static event Action OnRestartGame;

    public static event Action OnGameOver;

    public static event Action OnResetGameState;

    public static event Action OnGameFinished;

    public static event Action OnGameUnpaused;

    public static event Action OnGamePaused;

    public static event Action OnShowInfoScreen;

    public static event Action OnSoftResetGameState;


    // Bonuses

    public static event Action<float> OnChangePlatformWidthCatched;

    public static event Action<float, float> OnShootingPlatformСatched;

    public static event Action OnExtraLiveСatched;

    public static event Action<int> OnMultiBallСatched;

    // Audio

    public static event Action OnMasterVolumeChanged;

    public static event Action OnMusicVolumeChanged;

    public static event Action OnSFXVolumeChanged;



    public static void SoftResetGameStateEvent()
    {
        OnSoftResetGameState?.Invoke();
    }

    public static void RaiseScoreEvent(int score)
    {
        OnRaiseScore?.Invoke(score);
    }

    public static void RaiseHighScoreEvent(int score)
    {
        OnRaiseHighScore?.Invoke(score);
    }

    public static void ChangeLivesEvent(int lives)
    {
        OnChangeLives?.Invoke(lives);
    }

    public static void ChangeLevelEvent(int level)
    {
        OnChangeLevel?.Invoke(level);
    }

    public static void ResetGameStateEvent()
    {
        OnResetGameState?.Invoke();
    }

    public static void GameOverEvent()
    {
        OnGameOver?.Invoke();
    }

    public static void GameFinishedEvent()
    {
        OnGameFinished?.Invoke();
    }

    public static void GameContinueClickedEvent()
    {
        OnGameUnpaused?.Invoke();
    }

    public static void GamePauseClickedEvent()
    {
        OnGamePaused?.Invoke();
    }

    public static void NewGameClickedEvent()
    {
        OnNewGame?.Invoke();
    }

    public static void RestartGameClickedEvent()
    {
        OnRestartGame?.Invoke();
    }

    public static void ShowInfoScreenEvent()
    {
        OnShowInfoScreen?.Invoke();
    }


    // bonuses

    public static void ChangePlatformWithCatchEvent(float coef)
    {
        OnChangePlatformWidthCatched?.Invoke(coef);
    }

    public static void MultiBallCatchEvent(int generatedBallsNumber)
    {
        OnMultiBallСatched?.Invoke(generatedBallsNumber);
    }

    public static void ShootingPlatformCatchEvent(float duration, float fireCooldown)
    {
        OnShootingPlatformСatched?.Invoke(duration, fireCooldown);
    }

    public static void ExtraLiveCatchEvent()
    {
        OnExtraLiveСatched?.Invoke();
    }


    // Audio

    public static void MasterVolumeChangedEvent()
    {
        OnMasterVolumeChanged?.Invoke();
    }

    public static void MusicVolumeChangedEvent()
    {
        OnMusicVolumeChanged?.Invoke();
    }

    public static void SFXVolumeChangedEvent()
    {
        OnSFXVolumeChanged?.Invoke();
    }
}
