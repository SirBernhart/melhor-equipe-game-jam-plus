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
            transform.parent.GetComponent<IAMonstro>().anim.SetTrigger("kill");
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            Invoke("ChangeScene",1.6f);
        }
    }

    public void ChangeScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
