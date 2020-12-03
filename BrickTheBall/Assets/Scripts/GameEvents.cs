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

    public static void MultiBallCatchEvent(int generatedBallsNumber)
    {
        OnMultiBallСatch?.Invoke(generatedBallsNumber);
    }

    public static void ResetGameStateEvent()
    {
        OnResetGameState?.Invoke();
    }
}
