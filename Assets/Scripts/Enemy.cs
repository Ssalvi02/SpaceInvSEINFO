using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int actualLife;
    public int points;
    public float timeToShoot;
    public float maxTime;
    public float minTime;
    public float timer;
    public bool canShoot = false;
    public GameObject bullet;
    public Sprite deathIMG;
    public GameController gc;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        timeToShoot = Random.Range(maxTime, minTime);
        timer = timeToShoot;
        gc = GameObject.Find("Game").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (actualLife < 1) // Quando a bala atingir o inimigo a vida dele vai abaixar e se for menor que 1 o inimigo morre
        {
            Die();
        }
        else
        {
            Shoot();
        }
    }
    void Shoot()
    {
        if (canShoot)   // se pode atirar roda o if
        {
            canShoot = false;
            GameObject bulletOBJ = Instantiate(bullet, transform.position, Quaternion.identity);// nessa instanciação o inimigo define qual bala ele ira criar (ele sabe)/ está guardado
            bulletOBJ.GetComponent<Bullet>().speed = -bulletOBJ.GetComponent<Bullet>().speed;   // aqui para inverter a velocidade iremos deixa a velocidade negativa
            bulletOBJ.tag = "Enemy"; // como temos a bala salva podemos atribuir a ela uma tag para acertar apenas o player
        }
        else            // se não pode atirar roda o timer
        {
            if (timer > 0)// enquanto o timer for maior que 0 vai subtraindo do tempo e seta o canShoot para falso
            {
                canShoot = false;
                timer -= Time.deltaTime;
            }
            else    // se o timer for menor que zero resetamos ele para o valor inicial de timeToShoot e setamos canShoot para true
            {
                canShoot = true;
                timer = timeToShoot;
            }
        }
    }
    void Die()
    {
        GetComponent<SpriteRenderer>().sprite = deathIMG;  // nesse caso queremos que a animação de morte rode antes de destruir o objeto
        tag = "Untagged";
        Destroy(this.gameObject, 1);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            actualLife--;
            gc.game_points += points;
            Destroy(col.gameObject);
        }
    }
}
