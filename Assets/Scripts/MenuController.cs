using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject selector;
    private int pos_id = 0;
    private Vector3[] positions = { new Vector3(120, 97, 0f), new Vector3(120, 67, 0f), new Vector3(120, 35, 0f) };
    void Start()
    {
        selector = GameObject.Find("Canvas/Selector");
        selector.transform.localPosition = positions[pos_id];
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos_id++;
            if (pos_id > 2) //se estive na posição máxima volta para o início
            {
                pos_id = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos_id--;
            if (pos_id < 0)
            {
                pos_id = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))    // seleciona a fase
        {
            if (pos_id == 0)
            {
                SceneManager.LoadScene(1);
            }
            if (pos_id == 1)
            {
                SceneManager.LoadScene(2);
            }
            if (pos_id == 2)
            {
                Application.Quit();
            }
        }
        selector.transform.position = positions[pos_id];
    }
}
