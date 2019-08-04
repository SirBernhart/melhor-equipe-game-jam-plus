using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chave : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<ControleJogador>().numChaves++;
            Destroy(gameObject);
            this.GetComponent<AudioSource>().Play();
        }
    }
}
