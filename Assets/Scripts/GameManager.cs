using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] GameState gameState;
    [SerializeField] int score = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);

    }
    void Start()
    {
       
        SceneManager.activeSceneChanged += activeSceneChanged;
    }

    private void activeSceneChanged(Scene arg0, Scene arg1)
    {
        gameState = GameState.start;
        score = 0;
    }
    
    public void AddScore()
    {
        score +=10;
        
    }
    public int GetScore()
    {
        return score;
    }
    public GameState GetGameState()
    {
        return gameState;
    }
    public void SetGameState(GameState state)
    {
        gameState = state;
    }

}
public enum GameState : int
{
    None,
    start,
    finish

}
