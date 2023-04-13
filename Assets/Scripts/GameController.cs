using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public bool p1Die;
    public bool p2Die;
    public bool restart;
    public float timeToRestart;
    public int game_points = 0;
    public int numEnemys = 0;
    public PlayerController[] players;
    private TextMeshProUGUI text_pts;
    public GameObject loseUI;
    public GameObject WinUI;
    public GameObject canvas;

    void Start()
    {
        text_pts = GameObject.Find("Canvas/Score").GetComponent<TextMeshProUGUI>();
        text_pts.text = "Score: " + game_points;
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
        {
            timeToRestart += Time.deltaTime;
        }
        if (timeToRestart >= 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        text_pts.text = "Score: " + game_points;
        if (numEnemys <= 0)
        {
            Instantiate(WinUI, new Vector2(960, 540), Quaternion.identity, canvas.transform);
            restart = true;
        }

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].current_life > 0)
            {
                return;
            }
        }
        Instantiate(loseUI, new Vector2(960, 540), Quaternion.identity, canvas.transform);
        restart = true;
    }
}
