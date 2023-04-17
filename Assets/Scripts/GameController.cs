using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    enum GameState { Game, Restart };
    
    public float timeToRestart;
    public float n_restart = 0;
    public int game_points = 0;
    public int numEnemys = 0;
    public int players_count = 1;
    private TextMeshProUGUI text_pts;
    public GameObject loseUI;
    public GameObject WinUI;
    public GameObject canvas;

    private GameState gameState = GameState.Game;

    void Start()
    {
        text_pts = GameObject.Find("Canvas/Score").GetComponent<TextMeshProUGUI>();
        n_restart = PlayerPrefs.GetFloat("Resets");
        game_points = PlayerPrefs.GetInt("Score");
        text_pts.text = "Score: " + game_points;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        switch (gameState)
        {
            case GameState.Game:
                text_pts.text = "Score: " + game_points;

                if (numEnemys <= 0)
                {
                    Instantiate(WinUI, new Vector2(960, 540), Quaternion.identity, canvas.transform);
                    n_restart += 0.2f;
                    PlayerPrefs.SetInt("Score", game_points);
                    PlayerPrefs.SetFloat("Resets", n_restart);
                    gameState = GameState.Restart;
                }
                break;

            case GameState.Restart:
                timeToRestart += Time.deltaTime;

                if (timeToRestart >= 3)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                break;
        }
    }

    public void PlayerDestroyed()
    {
        players_count--;

        if (players_count <= 0)
        {
            PlayerPrefs.SetInt("Score", 0);
            Instantiate(loseUI, new Vector2(960, 540), Quaternion.identity, canvas.transform);
            gameState = GameState.Restart;
        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
