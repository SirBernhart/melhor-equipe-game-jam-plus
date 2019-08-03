using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleJogador : MonoBehaviour
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
        MovePersonagem();
        OlhaParaMouse();
    }

    private void MovePersonagem()
    {
        Vector3 novaPosicao = Vector3.zero;        

        if (Input.GetKey(KeyCode.W))
        {
            novaPosicao += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            novaPosicao += -Vector3.up;
        }
        if (Input.GetKey(KeyCode.D))
        {
            novaPosicao += Vector3.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            novaPosicao += -Vector3.right;
        }
        // Corrige o bônus de movimento diagonal
        novaPosicao = Vector3.ClampMagnitude((novaPosicao), 1.0f) * velocidade * Time.deltaTime + transform.position;

        rb.MovePosition(novaPosicao);
    }

    private void OlhaParaMouse()
    {
        Vector3 direcao = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
    }
}
