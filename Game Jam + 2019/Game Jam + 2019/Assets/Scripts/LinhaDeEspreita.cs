using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinhaDeEspreita : MonoBehaviour
{
    private IAMonstro ia;

    private void Awake()
    {
        ia = transform.parent.GetComponent<IAMonstro>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ia.MudarEstadoJogadorAvistado(false);
    }
}
