using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] ui_panels;
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
            SetState(GameState.AwaitingStart);
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
        currentState = newState;
        EnterState(currentState);
        OnGameStateChanged?.Invoke(currentState);
    }

    private void EnterState(GameState state)
    {
        switch (state)
        {
            case GameState.AwaitingStart:
                ui_panels[0].SetActive(true);
                break;
            case GameState.Playing:
                ui_panels[0].SetActive(false);
                break;
            case GameState.Win:
                ui_panels[1].SetActive(true);
                break;
            case GameState.Lose:
                ui_panels[2].SetActive(true);
                break;
        }
    }

    private void CountBlocks(int count)
    {
        Debug.Log($"{count}");
        if (count <= 0)
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
