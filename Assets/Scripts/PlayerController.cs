using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Rigidbody2D rb;
    public int speed = 10;
    public int maxX = 22;
    [Header("Shot")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private bool can_shoot = true;
    [SerializeField] private float shot_time = 1f;
    [Header("Life")]
    [SerializeField] public int current_life;
    [SerializeField] private Sprite death_img;
    [Header("LifeSprites")]
    [SerializeField] private RectTransform lifesIcon;
    [SerializeField] public TMP_Text n_life;
    [Header("GameController")]
    public GameController gc;

    public KeyCode keyLeft;
    public KeyCode keyRight;
    public KeyCode keyFire;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gc = GameObject.Find("Game").GetComponent<GameController>();

        speed += Mathf.CeilToInt(gc.n_restart);
        if (speed > 20)
        {
            speed = 20;
        }
        UpdateHud();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerShoot();
    }

    private void PlayerShoot()
    {
        if (Input.GetKeyDown(keyFire) && can_shoot)
        {
            can_shoot = false;
            shot_time = 1f - gc.n_restart;
            if (shot_time <= 0.4f)
            {
                shot_time = 0.4f;
            }
            GameObject bulletOBJ = Instantiate(bullet,
                transform.position + new Vector3(0, 2, 0), Quaternion.identity);

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
        rb.velocity = new Vector2(0f, 0f);
        if (Input.GetKey(keyLeft))
        {
            rb.velocity = new Vector2(-speed, 0f);
        }
        else if (Input.GetKey(keyRight))
        {
            rb.velocity = new Vector2(speed, 0f);
        }

        Vector3 pos = rb.transform.position;
        if (pos.x < -maxX)
        {
            pos.x = -maxX;
        }
        else if (pos.x > maxX)
        {
            pos.x = maxX;
        }
        rb.transform.position = pos;


    }

    void TakeLife()
    {
        current_life--;
        UpdateHud();
        if (current_life <= 0)
        {
            Death();
            gc.PlayerDestroyed();
        }
    }

    public void UpdateHud()
    {
        lifesIcon.sizeDelta = new Vector2(current_life * 100, lifesIcon.sizeDelta.y);
        n_life.text = current_life.ToString();
    }

    public void Death()
    {
        GetComponent<SpriteRenderer>().sprite = death_img;  // nesse caso queremos que a animação de morte rode antes de destruir o objeto
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 0.8f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        TakeLife();
        Destroy(col.gameObject);
    }
}
