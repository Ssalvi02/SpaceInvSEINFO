using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    public float delay;
    public bool canMove;
    public float timeToMove;
    public float amountMove;
    public float maxRange;
    bool left;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartMove());
        left = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
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
            StartCoroutine(CooldownMove());
            canMove = false;
        }
    }
    void GoToFront()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - 2);
    }
    public IEnumerator StartMove()
    {

        yield return new WaitForSeconds(delay);
        StartCoroutine(CooldownMove());
    }
    public IEnumerator CooldownMove()    // Esta é uma corotina(coroutines) elas param o código por um determinado tempo
    {
        yield return new WaitForSeconds(timeToMove);   // e depois desse tempo continua
        canMove = true;
    }
}
