using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            transform.parent.GetComponent<IAMonstro>().alvo = null;
            collision.gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Destroy(collision.gameObject);
        }
    }
}
