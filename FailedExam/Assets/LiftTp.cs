using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiftTp : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    Animator planim;
    public Scene scene;
    private bool _enabled;
    public string _name;
    void Start()
    {   
        animator = GetComponent<Animator>();
        scene = SceneManager.GetActiveScene();
        if(scene.name == "SampleScene")
        {
           _enabled = true;
        }
        else
        {
            _enabled = false;
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.tag == "Player")
            {
                planim = collision.gameObject.GetComponent<Animator>();

                if (planim.GetBool("isSafety") == true)
                {
                    if (_enabled)
                    {
                        if (collision.tag == "Player")
                        {
                            animator.SetBool("isOpen", true);
                        }
                    }
                    else
                    {
                        // Debug.Log("tp disabled");
                    }

                }
                else
                {
                    // Debug.Log("not safety");
                }
            }
            

        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("isOpen", false);
        }
    }
    void TpPlayer()
    {
        SceneManager.LoadScene(_name);
    }
}
