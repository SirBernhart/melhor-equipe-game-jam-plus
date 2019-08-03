using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocidade = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 novaPosicao = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            novaPosicao += Vector3.up * velocidade * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            novaPosicao += -Vector3.up * velocidade * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            novaPosicao += Vector3.right * velocidade * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            novaPosicao += -Vector3.right * velocidade * Time.deltaTime;
        }

        rb.MovePosition(novaPosicao);
    }
}
