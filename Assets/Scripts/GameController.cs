using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public int game_points = 0;
    private TextMeshProUGUI text_pts;

    void Start()
    {
        text_pts = GameObject.Find("Canvas/Score").GetComponent<TextMeshProUGUI>();
        text_pts.text = "Score: " + game_points;
    }

    // Update is called once per frame
    void Update()
    {
        text_pts.text = "Score: " + game_points;
    }
}
