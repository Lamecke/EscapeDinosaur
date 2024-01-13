using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText, scoreBoard;
    [SerializeField] GameObject uiMenu;
    [SerializeField] GameObject uiGame;
    [SerializeField] AdManager adManager;



    void Start()
    {
        adManager = FindObjectOfType<AdManager>();
        if (GameManager.Instance.GetGameState() == GameState.None)
        {
            uiMenu.SetActive(true);
        }
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.GetGameState() == GameState.finish)
        {
            scoreText.text = $"Score : {GameManager.Instance.GetScore()}";
            EnableUiGame();
        }
        scoreBoard.text = $"Score : {GameManager.Instance.GetScore()}";

    }
    public void EnableUiGame()
    {
        uiGame.SetActive(true);
    }
    public void StartGame()
    {
        GameManager.Instance.SetGameState(GameState.start);
        uiMenu.SetActive(false);
    }
    public void RestartGame()
    {
        adManager.ShowAd();
        SceneManager.LoadScene(0);
    }

}
