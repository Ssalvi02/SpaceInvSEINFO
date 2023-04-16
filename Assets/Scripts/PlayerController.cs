using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [Header("Life")]
    [SerializeField] public int current_life;
    [SerializeField] private Sprite death_img;
    [Header("LifeSprites")]
    [SerializeField] private GameObject[] lifesp1;
    [SerializeField] private GameObject[] lifesp2;
    [SerializeField] private GameObject n_lifep1;
    [SerializeField] private GameObject n_lifep2;
    [Header("2Players")]
    [SerializeField] private bool sec_player = false;
    [Header("GameController")]
    public GameController gc;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gc = GameObject.Find("Game").GetComponent<GameController>();
        
        speed += Mathf.CeilToInt(gc.n_restart);
        if(speed > 20)
        {
            speed = 20;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (current_life < 1)// Quando a bala atingir o inimigo a vida dele vai abaixar e se for menor que 1 o inimigo morre
        {
            Death();
        }
        else
        {
            if (!sec_player)
            {
                PlayerOneMovement();
                PlayerOneShoot();
            }
            else
            {
                PlayerTwoMovement();
                PlayerTwoShoot();
            }
        }

    }

    private void PlayerOneShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && can_shoot)
        {
            can_shoot = false;
            shot_time = 1f - gc.n_restart;
            if(shot_time <= 0.4f)
            {
                shot_time = 0.4f;
            }
            GameObject bulletOBJ = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletOBJ.tag = "Player";
        }

        shot_time -= Time.deltaTime;

        if (shot_time < 0)
        {
            can_shoot = true;
        }
    }

    private void PlayerTwoShoot()
    {
        if (Input.GetKeyDown(KeyCode.L) && can_shoot)
        {
            can_shoot = false;
            shot_time = 1f - gc.n_restart;
            if (shot_time <= 0.4f)
            {
                shot_time = 0.4f;
            }
            GameObject bulletOBJ = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletOBJ.tag = "Player";
        }

        shot_time -= Time.deltaTime;

        if (shot_time < 0)
        {
            can_shoot = true;
        }
    }

    private void PlayerOneMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, 0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, 0f);
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
        }
    }
    private void PlayerTwoMovement()
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
    void TakeLifeP1()
    {
        if (current_life > 0)
        {
            lifesp1[current_life - 1].SetActive(false);
            current_life--;

        }
        n_lifep1.GetComponent<TMP_Text>().text = current_life.ToString();
    }
    void TakeLifeP2()
    {
        if (current_life >= 0)
        {
            lifesp2[current_life - 1].SetActive(false);
            current_life--;
        }
        n_lifep2.GetComponent<TMP_Text>().text = current_life.ToString();
    }
    void Death()
    {
        GetComponent<SpriteRenderer>().sprite = death_img;  // nesse caso queremos que a animação de morte rode antes de destruir o objeto
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 1);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy" && !sec_player)
        {
            TakeLifeP1();
            Destroy(col.gameObject);
        }
        if (col.tag == "Enemy" && sec_player)
        {
            TakeLifeP2();
            Destroy(col.gameObject);
        }
    }
}
