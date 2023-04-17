using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int actualLife;
    public int base_points;//futuro
    public int points;  //futuro
    public float timeToShoot;   
    public float maxTimeTS; //randomificar tiro
    public float minTimeTS;
    public float timer; // variável para ver o timer
    public bool canShoot = false;   //trigger para tiro
    bool die = false;   
    public GameObject bullet;   
    public Sprite deathIMG;
    public GameController gc;   //furuto

    // Start is called before the first frame update
    void Start()
    {
        timeToShoot = Random.Range(maxTimeTS, minTimeTS);
        timer = timeToShoot;
        gc = GameObject.Find("Game").GetComponent<GameController>();    //futuro
        points = base_points + (Mathf.CeilToInt(gc.n_restart) * 5);     //futuro

    }

    // Update is called once per frame
    void Update()
    {
        if (actualLife < 1 && die == false) // Quando a bala atingir o inimigo a vida dele vai abaixar e se for menor que 1 o inimigo morre
        {
            Die();
            die = true; //trigger para não poder atirar
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
            GameObject bulletOBJ = Instantiate(bullet, transform.position, Quaternion.identity);// nessa instanciação o inimigo define a bala que ele ira criar / está guardado
            bulletOBJ.GetComponent<Bullet>().speed = -bulletOBJ.GetComponent<Bullet>().speed;   // como usamos a mesma bullet deve-mos apenas inverter a velocidade 
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
        tag = "Untagged";   // serve para o inimigo não interagir mais com a bullet
        gc.numEnemys--;     //futuro
        Destroy(this.gameObject, 0.4f); 
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            actualLife--;
            gc.game_points += points;
            Destroy(col.gameObject);    //destoi a bullet que entra em contato com o player
        }
    }
}
