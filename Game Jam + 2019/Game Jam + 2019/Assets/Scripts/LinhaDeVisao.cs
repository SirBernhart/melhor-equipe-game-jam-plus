using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinhaDeVisao : MonoBehaviour
{
    private IAMonstro ia;
    private CircleCollider2D col;
    public float anguloLinhaDeVisao = 45f;
    private void Awake()
    {
        ia = transform.parent.GetComponent<IAMonstro>();
        col = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        // Se o jogador está perto o suficiente para ser visto
        if(collision.tag == "Player")
        {
            Vector3 direction = collision.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.right);

            // Se o jogador estiver dentro do angulo de visão na frente do monstro
            if(angle < anguloLinhaDeVisao * 0.5f)
            {
                RaycastHit2D hit;
                hit = Physics2D.Raycast(transform.position, direction, col.radius);
                // Se não houver nada no caminho entre o monstro e o jogador
                if(hit.collider.tag == "Player")
                {
                    ia.MudarEstadoJogadorAvistado(true);
                }
                // Se não estiver sendo visto pelo monstro
                else
                    ia.MudarEstadoJogadorAvistado(false);
            }
            
        }
    }
}
