using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chave : MonoBehaviour
{
public AudioSource keys;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            keys.Play();
            collision.GetComponent<ControleJogador>().numChaves++;
            Destroy(gameObject);
        }
    }
}
