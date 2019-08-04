using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esconderijo : MonoBehaviour
{
    private Vector3 posicaoAnterior;
    private bool escondido;
    private GameObject jogador;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            jogador = collision.gameObject;
            jogador.SetActive(false);

            posicaoAnterior = collision.transform.position;
            jogador.transform.position = transform.position;
            jogador.GetComponent<ControleJogador>().escondido = true;

            StartCoroutine(GerenciaJogadorEscondido());
        }
    }

    // Usado para poder desabilitar o gameObject do jogador 
    IEnumerator GerenciaJogadorEscondido()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                jogador.transform.position = posicaoAnterior;
                jogador.GetComponent<ControleJogador>().escondido = false;
                jogador.SetActive(true);
                gameObject.SetActive(false);
                gameObject.SetActive(true);
                StopCoroutine(GerenciaJogadorEscondido());
            }
            yield return null;
        }
    }


}
