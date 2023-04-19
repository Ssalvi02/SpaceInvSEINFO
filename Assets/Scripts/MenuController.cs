using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;

public class MenuController : MonoBehaviour
{
    public GameObject menuContainer;
    public Color newColor = Color.yellow;

    public int pos_id = 0;
    public TextMeshProUGUI[] texts;
    public int[] scenes = { 1, 2, -1 };

    private void Start()
    {
        texts = menuContainer.GetComponentsInChildren<TextMeshProUGUI>();
    }
    void SetSelectedItem(int index, bool selected)
    {
        if (selected)
        {
            texts[index].color = newColor;
        }
        else
        {
            texts[index].color = Color.white;
        }
    }

    void Update()
    {
        SetSelectedItem(pos_id, false);
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos_id++;
            if (pos_id >= texts.Length) // menu circular
            {
                pos_id = 0;
            }     
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos_id--;
            if (pos_id < 0)
            {
                pos_id = texts.Length - 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))    // seleciona opção
        {
            int scn = scenes[pos_id];


            if (scn == -1)
            {
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene(scn);
            }
        }
        SetSelectedItem(pos_id, true);
    }
}