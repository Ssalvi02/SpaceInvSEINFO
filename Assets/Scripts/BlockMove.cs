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
    public GameController gc;
    bool startMove;
    bool left;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        startMove = true;
        left = false;
        rb = GetComponent<Rigidbody2D>();
        gc = GameObject.Find("Game").GetComponent<GameController>();    // futuro
        timeToMove -= gc.n_restart;                                     // futuro
        if (timeToMove <= 0.8f) // clamp
        {
            timeToMove = 0.8f;
        }
        timer = timeToMove;
    }

    // Update is called once per frame
    void Update()
    {
        if (startMove == true)  //trigger inicial para se movimentar pois queremos que cara bloco se mova em um tempo inicial diferente
        {                       // e queremos que ele rode apenas no começo
            DelayMove();        
        }
        else
        {
            if (canMove)
            {
                Move();
            }
            CooldownMove(); // essa função seta ao valor inicial do contador 
        }
    }
    void GoToFront()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - amountMove);
        if (transform.position.y <= -2) //quando o bloco estiver em certa posição o player perde o jogo
        {
            GameObject.Find("player").GetComponent<PlayerController>().current_life = 0;
            GameObject.Find("player").GetComponent<PlayerController>().UpdateHud();
            GameObject.Find("player").GetComponent<PlayerController>().Death();
            gc.PlayerDestroyed();
        }
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
    void Move() // depois de se mover ele não pode mais se movimentar até que o contador chegue a zero
    {
        if (left)   //para qual lado o bloco deve ir
        {
            transform.position = new Vector2(transform.position.x - amountMove, transform.position.y); // se for parar a esquerda soma-se com a quantidade de nmov
            if (transform.position.x <= -maxRange)    //verifica se o bloco ja chegou em X maximo            
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
        canMove = false;    //não deixa o inimigo se mover no update
    }
}
