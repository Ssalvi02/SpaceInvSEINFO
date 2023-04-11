using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject selector;
    private int pos_id = 0;
    private Vector3[] positions = { new Vector3(760f, 530f, 0f), new Vector3(680f, 380f, 0f), new Vector3(800f, 240f, 0f)};
    void Start()
    {
        selector = GameObject.Find("Canvas/Selector");
        selector.transform.position = positions[pos_id];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            pos_id++;
            if(pos_id > 2)
            {
                pos_id = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos_id--;
            if(pos_id < 0)
            {
                pos_id = 0;
            }
        }
        selector.transform.position = positions[pos_id];
    }
}
