using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    public Sprite deathIMG;
    public GameObject numLife;
    public GameObject[] lifes;
    public int actualLife;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (actualLife < 1) // Quando a bala atingir o inimigo a vida dele vai abaixar e se for menor que 1 o inimigo morre
        {
            GetComponent<SpriteRenderer>().sprite = deathIMG;  // nesse caso queremos que a animação de morte rode antes de destruir o objeto
            Destroy(this.gameObject, 1);
        }
        else
        {
            PlayerMovement();
            Shoot();
        }

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
    void takeLife()
    {
        lifes[actualLife - 1].SetActive(false);
        actualLife--;
        numLife.GetComponent<TMP_Text>().text = actualLife.ToString();

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            takeLife();
            Destroy(col.gameObject);
        }
    }
}
