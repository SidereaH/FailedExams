using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject dialogue;
    bool continued = false;
    bool isEnter;
    DamageableCharacter collisionplayer;
    Opener open;
    void Start()
    {

        collisionplayer = GameObject.FindGameObjectWithTag("Player").gameObject.transform.GetComponent<DamageableCharacter>(); 
        open = gameObject.transform.GetComponent<Opener>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnter)
        {
            if(Keyboard.current.fKey.wasPressedThisFrame)
            {
                dialogue.SetActive(true);
                collisionplayer.SetPause();
                continued = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {   
            isEnter = true;
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEnter = false;

            if (continued)
            {
                collisionplayer.UnPause();
                open.Open();
            }
            
        }
    }
}
