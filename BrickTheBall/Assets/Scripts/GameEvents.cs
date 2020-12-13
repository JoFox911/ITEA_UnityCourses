using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnAllBricksDestroyed;

    public static event Action OnAllBallsWasted;

    public static event Action<Brick> OnBrickDestructed;

    public static event Action<int> OnRaiseScore;

    public static event Action<int> OnRaiseHighScore;

    public static event Action<int> OnChangeLives;

    public static event Action<int> OnChangeLevel;

    public static event Action<int> OnMultiBallСatch;

    public static event Action OnResetGameState;

    public static event Action OnGameOver;

    public static event Action OnGameFinished;

    public static event Action OnGameUnpaused;

    public static event Action OnGamePaused;

    public static event Action OnNewGameClickedEvent;

    public static event Action<float> OnChangePlatformWithCatchEvent;

    public static event Action<float, float> OnShootingPlatformСatch;

    public static event Action OnExtraLiveСatch;

    public static event Action OnRestartGameClicked;

    public static event Action<int> OnEnemyDestroyed;

    public static event Action OnShowInfoScreen;    

    // Audio
    public static event Action OnMasterVolumeChanged;

    public static event Action OnMusicVolumeChanged;

    public static event Action OnSFXVolumeChanged;

    

    public static void AllBricksDestroyedEvent()
    {
        OnAllBricksDestroyed?.Invoke();
    }

    public static void AllBallsWastedEvent()
    {
        OnAllBallsWasted?.Invoke();
    }

    public static void BrickDestructedEvent(Brick brick)
    {
        OnBrickDestructed?.Invoke(brick);
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
        Debug.Log("NewGameClickedEvent");
        OnNewGameClickedEvent?.Invoke();
    }

    public static void RestartGameClickedEvent()
    {
        OnRestartGameClicked?.Invoke();
    }


    // bonuses
    public static void ChangePlatformWithCatchEvent(float coef)
    {
        OnChangePlatformWithCatchEvent?.Invoke(coef);
    }

    public static void MultiBallCatchEvent(int generatedBallsNumber)
    {
        OnMultiBallСatch?.Invoke(generatedBallsNumber);
    }

    public static void ShootingPlatformCatchEvent(float duration, float fireCooldown)
    {
        OnShootingPlatformСatch?.Invoke(duration, fireCooldown);
    }

    public static void ExtraLiveCatchEvent()
    {
        OnExtraLiveСatch?.Invoke();
    }

    public static void EnemyDestroyedEvent(int points)
    {
        OnEnemyDestroyed?.Invoke(points);
    }
    public static void ShowInfoScreenEvent()
    {
        OnShowInfoScreen?.Invoke();
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
