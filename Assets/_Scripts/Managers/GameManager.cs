using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    void Awake() => Instance = this;

    private void OnEnable()
    {
    }


    private void OnDisable()
    {
    }

    private void Start()
    {
        UpdateGameState(GameState.Inside);
    }

    public void UpdateGameState(GameState newState)
    {
        Debug.Log($"UpdateGameState: {State} > {newState}");

        State = newState;

        switch (newState)
        {
            case GameState.GameStart:
                HandleGameStart();
                break;
            case GameState.Inside:
                HandleInside();
                break;
            case GameState.InRest:
                HandleRest();
                break;
            case GameState.InConversation:
                HandleConversation();
                break;
            case GameState.WinState:
                HandleWin();
                break;
            case GameState.LoseState:
                HandleLose();
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleGameStart()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void HandleInside ()
    {
    }

    private void HandleRest()
    {
    }

    private void HandleConversation()
    {
    }

    private void HandleWin()
    {
    }

    private void HandleLose()
    {
    }
}

public enum GameState
{
    GameStart,
    Inside,
    InRest,
    InConversation,
    WinState,
    LoseState
}