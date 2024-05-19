using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWindow : MonoBehaviour
{
    
    Collider2D detectEveryoneCol;
    public List<DamageableCharacter> detectedObjs = new List<DamageableCharacter>();
    void Start()
    {
        detectEveryoneCol = GetComponent<Collider2D>();
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            DamageableCharacter character = collision.GetComponent<DamageableCharacter>();
           
            if (character != null)
            {
                detectedObjs.Add(character);
               
            }
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            
            DamageableCharacter character = collision.GetComponent<DamageableCharacter>();
            detectedObjs.Remove(character);
            if (character != null)
            {
                character.UnPause();
            }
        }
    }
  
}
