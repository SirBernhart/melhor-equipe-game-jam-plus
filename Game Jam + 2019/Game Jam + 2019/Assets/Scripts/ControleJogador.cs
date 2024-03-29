﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControleJogador : MonoBehaviour
{
    public bool foiVisto;

    private Rigidbody2D rb;
    public float velocidade = 3f;

    public Animator chavesUI;

    public int numChaves;
    //[HideInInspector]
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rotacaoAtual = transform.rotation.eulerAngles;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePersonagem();
        chavesUI.SetInteger("Chaves", numChaves);
    }

    private void MovePersonagem()
    {
        Vector3 novaPosicao = Vector3.zero;  

        anim.SetBool("moving", false);      

        if (Input.GetButton("Up"))
        {
            novaPosicao += Vector3.up;
            anim.SetBool("moving", true);
        }
        if (Input.GetButton("Down"))
        {
            novaPosicao += -Vector3.up;
            anim.SetBool("moving", true);
        }
        if (Input.GetButton("Right"))
        {
            novaPosicao += Vector3.right;
            anim.SetBool("moving", true);
        }
        if (Input.GetButton("Left"))
        {
            novaPosicao += -Vector3.right;
            anim.SetBool("moving", true);
        }
        RotacionaPersonagem(novaPosicao);
        // Corrige o bônus de movimento diagonal
        novaPosicao = Vector3.ClampMagnitude((novaPosicao), 1.0f) * velocidade * Time.deltaTime + transform.position;

        rb.MovePosition(novaPosicao);
    }

    [SerializeField] private float tempoDeSuavizacaoRotacao = .12f;
    Vector3 velocidadeDaSuavizacaoRotacao;
    Vector3 rotacaoAtual;
    private void RotacionaPersonagem(Vector3 novaPosicao)
    {
        //rotacaoAtual = transform.rotation.eulerAngles;
        rotacaoAtual = Vector3.SmoothDamp(rotacaoAtual, new Vector3(novaPosicao.x, novaPosicao.y),
            ref velocidadeDaSuavizacaoRotacao, tempoDeSuavizacaoRotacao);
        float angulo = Mathf.Atan2(rotacaoAtual.y, rotacaoAtual.x) * Mathf.Rad2Deg;
        rb.MoveRotation(angulo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Finish")
        {
            //Debug.Log("Ganhou!");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene("00 - Title");
        }
    }
}
