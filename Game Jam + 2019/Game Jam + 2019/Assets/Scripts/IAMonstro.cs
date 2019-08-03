﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class IAMonstro : MonoBehaviour
{
    public Transform alvo;

    public float velocidadePatrulha = 2f;
    public float velocidadePersseguicao = 3f;
    public float distanciaProxWaypoint = 3f;
    public bool jogadorEmVisao;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    private bool updatePathExecutando; // Se a corotina UpdatePath está sendo executada

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(UpdatePath());
    }

    public void MudarEstadoJogadorAvistado(bool avistado)
    {
        jogadorEmVisao = avistado;
        if (avistado)
        {
            Debug.Log(updatePathExecutando);
            if (updatePathExecutando)
            {
                updatePathExecutando = false;
                StopCoroutine(UpdatePath());
            }
        }
        else
        {
            // Se a corotina já estiver sendo executada, não iniciar outra
            if (!updatePathExecutando)
            {
                StartCoroutine(UpdatePath());
            }
        }
    }

    IEnumerator UpdatePath()
    {
        updatePathExecutando = true;
        while(true)
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, alvo.position, OnPathComplete);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnPathComplete (Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        } 
    }

    [SerializeField] private float tempoDeSuavizacaoRotacao = .12f;
    Vector3 velocidadeDaSuavizacaoRotacao;
    Vector3 rotacaoAtual;
    void Update()
    {
        // Seguir o jogador
        if (jogadorEmVisao)
        {
            Vector3 dir = (alvo.position - transform.position).normalized;
            float angulo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rb.MoveRotation(Quaternion.AngleAxis(angulo, Vector3.forward));

            rb.MovePosition(transform.position + dir * velocidadePersseguicao * Time.deltaTime);
        }
        // Patrulhar
        else
        {
            if (path == null)
                return;

            if(currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direcao = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

            rotacaoAtual = Vector3.SmoothDamp(rotacaoAtual, new Vector3(direcao.x, direcao.y),
            ref velocidadeDaSuavizacaoRotacao, tempoDeSuavizacaoRotacao);

            float angulo = Mathf.Atan2(rotacaoAtual.y, rotacaoAtual.x) * Mathf.Rad2Deg;
            rb.SetRotation(angulo);

            rb.MovePosition(rb.position + direcao * velocidadePatrulha * Time.deltaTime);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if(distance < distanciaProxWaypoint)
            {
                currentWaypoint++;
            }
        }
    }
}
