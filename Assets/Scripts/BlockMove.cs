using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    public bool canMove;
    public float delay;
    public float timeToMove;
    public float timer;
    public float amountMove;
    public float maxRange;
    bool startMove;
    bool left;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        startMove = true;
        left = false;
        rb = GetComponent<Rigidbody2D>();
        timer = timeToMove;
    }

    // Update is called once per frame
    void Update()
    {
        if (startMove == true)
        {
            DelayMove();
        }
        else
        {
            if (canMove)
            {
                Move();
            }
            CooldownMove();
        }
    }
    void GoToFront()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - 2);
    }
    void DelayMove()
    {
        if (delay > 0)// enquanto o timer for maior que 0 vai subtraindo do tempo e seta o canShoot para falso
        {
            canMove = false;
            delay -= Time.deltaTime;
        }
        else    // se o timer for menor que zero resetamos ele para o valor inicial de timeToShoot e setamos canShoot para true
        {
            Move();
            startMove = false;
        }
    }
    void CooldownMove()    // Esta é uma corotina(coroutines) elas param o código por um determinado tempo
    {
        if (timer > 0)// enquanto o timer for maior que 0 vai subtraindo do tempo e seta o canShoot para falso
        {
            canMove = false;
            timer -= Time.deltaTime;
        }
        else    // se o timer for menor que zero resetamos ele para o valor inicial de timeToShoot e setamos canShoot para true
        {
            timer = timeToMove;
            canMove = true;
        }
    }
    void Move()
    {
        if (left)   //para qual lado o bloco deve ir
        {
            transform.position = new Vector2(transform.position.x - amountMove, transform.position.y); // se for parar a esquerda soma-se com a quantidade de nmov
            if (transform.position.x <= -maxRange)    //verifica se o bloco ja chegou no maximo            
            {
                GoToFront();
                left = false;
            }
        }
        else
        {
            transform.position = new Vector2(transform.position.x + amountMove, transform.position.y); // se for parar a direita subtrai
            if (transform.position.x >= maxRange)
            {
                GoToFront();
                left = true;
            }
        }
        canMove = false;
    }
}
