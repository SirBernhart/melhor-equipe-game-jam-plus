using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent.GetComponent<IAMonstro>().alvo = null;
        collision.gameObject.SetActive(false);
        //Destroy(collision.gameObject);
    }
}
