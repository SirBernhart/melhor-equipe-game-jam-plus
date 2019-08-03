using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            int numChaves = collision.GetComponent<ControleJogador>().numChaves;

            if (numChaves > 0)
            {
                collision.GetComponent<ControleJogador>().numChaves--;
                Destroy(gameObject);
            }
        }
    }
}
