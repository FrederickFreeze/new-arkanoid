using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<GameState> OnGameStateChanged;
    public enum GameState
    {
        AwaitingStart,  // Ожидание начала
        Playing,        // Игровой процесс
        Win,            // Победа
        Lose            // Поражение
    }
    private static GameState currentState;
    public static GameManager instance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            currentState = GameState.AwaitingStart;
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        BlockState.OnBlockDestroyed += CountBlocks;
    }

    private void OnDestroy()
    {
        BlockState.OnBlockDestroyed -= CountBlocks;
    }

    public void SetState(GameState newState)
    {
        ExitState(currentState);
        currentState = newState;
        EnterState(currentState);
        OnGameStateChanged?.Invoke(currentState);
    }

    private void ExitState(GameState state)
    {
        
    }

    private void EnterState(GameState state)
    {
        
    }

    private void CountBlocks(bool isDestroyed)
    {
        if(FindObjectsByType<BlockState>(FindObjectsSortMode.None).Length <= 0)
        {
            SetState(GameState.Win);
            Debug.Log($"Победа");
        }
    }

    private bool CanTransitionTo(GameState newState)
    {
        var allowedTransitions = new Dictionary<GameState, GameState[]>
        {
            { GameState.AwaitingStart, new[] { GameState.Playing } },
            { GameState.Playing, new[] { GameState.Win, GameState.Lose} },
            { GameState.Win, new[] { GameState.AwaitingStart} },
            { GameState.Lose, new[] { GameState.AwaitingStart} }
        };

        return Array.Exists<GameState>(allowedTransitions[currentState], x => x == newState);
    }

    public bool IsCurrentState(GameState state)
    {
        return currentState == state;
    }
}
