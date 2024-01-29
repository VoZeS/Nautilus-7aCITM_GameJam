using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnredaderaScript : MonoBehaviour
{
    // Start is called before the first frame update
    

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
}
