using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public AudioSource Portas;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            int numChaves = collision.gameObject.GetComponent<ControleJogador>().numChaves;

            if (numChaves > 0)
            {
                collision.gameObject.GetComponent<ControleJogador>().numChaves--;
                Portas.Play();
                Destroy(gameObject);
            }
        }
    }
}
