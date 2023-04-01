using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Rigidbody2D rb;
    public int speed = 10;
    [Header("Shot")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private bool can_shoot = true;
    [SerializeField] private float shot_time = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && can_shoot)
        {
            can_shoot = false;
            shot_time = 1f;
            GameObject bulletOBJ = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletOBJ.tag = "Player";
        }

        shot_time -= Time.deltaTime;

        if (shot_time < 0)
        {
            can_shoot = true;
        }
    }

    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-speed, 0f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(speed, 0f);
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
        }
    }
}
