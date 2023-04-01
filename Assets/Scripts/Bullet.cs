using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 4);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector2(0, 1) * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.tag);

        if ((tag.StartsWith("P") && collision.collider.name.StartsWith("e")) || (tag == "Enemy" && collision.collider.tag == "Player"))
        {
            Debug.Log("DESTROI");
            Destroy(gameObject);
        }
    }
}
