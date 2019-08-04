using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esconderijo : MonoBehaviour
{
    private Vector3 posicaoAnterior;
    private bool escondido;
    public bool foiVisto;
    private GameObject jogador;
    private ControleJogador controle;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            jogador = collision.gameObject;
            controle = jogador.GetComponent<ControleJogador>();
            foiVisto = controle.foiVisto; // Usado para saber se o monstro o encontrará mesmo estando escondido ou não
            jogador.SetActive(false);

            posicaoAnterior = collision.transform.position;
            jogador.transform.position = transform.position;

            StartCoroutine(GerenciaJogadorEscondido());
        }

        if(collision.gameObject.tag == "Monstro")
        {
            if (foiVisto)
            {
                jogador.SetActive(true);
            }
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
                jogador.SetActive(true);
                gameObject.SetActive(false);
                gameObject.SetActive(true);
                StopCoroutine(GerenciaJogadorEscondido());
            }
            yield return null;
        }
    }


}
