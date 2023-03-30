using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int actualLife;
    public float timeToShoot;
    public bool canShoot = false;
    public GameObject bullet;
    Rigidbody2D rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ShootTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (actualLife < 1) // Quando a bala atingir o inimigo a vida dele vai abaixar e se for menor que 1 o inimigo morre
        {
            anim.SetBool("Die", true);  // nesse caso queremos que a animação de morte rode antes de destruir o objeto
            Destroy(this.gameObject, 1);
        }
        if (canShoot == true)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        canShoot = false;
        GameObject bulletOBJ = Instantiate(bullet, transform.position, Quaternion.identity);// nessa instanciação o inimigo define qual bala ele ira criar (ele sabe)/ está guardado
        bulletOBJ.GetComponent<Bullet>().speed = -bulletOBJ.GetComponent<Bullet>().speed;   // aqui para inverter a velocidade iremos deixa a velocidade negativa
        bulletOBJ.tag = "Enemy"; // como temos a bala salva podemos atribuir a ela uma tag para acertar apenas o player
        StartCoroutine(ShootTime());
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            actualLife--;
        }
    }
    public IEnumerator ShootTime()    // Esta é uma corotina(coroutines) elas param o código por um determinado tempo
    {
        yield return new WaitForSeconds(Random.Range(timeToShoot, 20));   // e depois desse tempo continua
        canShoot = true;
    }
}
