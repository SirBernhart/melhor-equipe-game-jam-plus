using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAudio : MonoBehaviour
{

    public AudioSource Select;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlaySound()
    {
        Select.Play();
    }
}
