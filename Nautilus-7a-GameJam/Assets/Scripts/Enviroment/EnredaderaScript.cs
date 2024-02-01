using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnredaderaScript : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource bushSound;
    public AudioSource bushSound2;


    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
            animator.SetBool("ActivarAnimacion", true);
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
            animator.SetBool("ActivarAnimacion", false);
        
    }
    public void SoundBush()
    {

        int indiceAleatorio = Random.Range(0, 1);

        switch (indiceAleatorio)
        {
            case 0:
                bushSound.Play();
                break;

            case 1:
                bushSound2.Play();
                break;
            default:
                break;

        }
    }
}
