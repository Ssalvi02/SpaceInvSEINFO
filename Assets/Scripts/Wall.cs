using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int life;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {
            life--;
            if (life < 1)
            {
                Destroy(this.gameObject);
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = sprites[life - 1];
            }

    }
}
