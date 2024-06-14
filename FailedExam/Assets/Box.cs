using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Collider2D collider;
    [SerializeField] GameObject[] objcst;
    Animator animator;
    bool isOpened;
    void Start()
    {
        collider = transform.GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
       if(other.tag == "Player")
        {
            if(other.GetComponent<Animator>().GetBool("isSafety") == true)
            if (isOpened == false)
            {
                
                int i = Random.Range(0, objcst.Length);
                animator.SetTrigger("Opening");
                animator.SetBool("IsOpened", true);
                Instantiate(objcst[i], transform.position, Quaternion.identity);
                isOpened = true;
            }
        }
       

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //animator.SetBool("IsOpened", true);
    }
}
