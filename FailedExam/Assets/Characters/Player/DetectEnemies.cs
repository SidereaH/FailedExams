using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemies : MonoBehaviour
{
    public string tagTarget = "Enemy";
    public List<Collider2D> detectedEnObjs = new List<Collider2D>();
    public Animator playerAnimator;
    public Collider2D col;
    public GameObject player;
    public bool isDanger = true;
    DamageableCharacter character;
    GameObject gun;
    // Start is called before the first frame update
    void Start()
    {
        if (player.transform.childCount == 2)
        {
            gun = player.transform.GetChild(1).gameObject;
            gun.GetComponent<SpriteRenderer>().enabled = false;
        }
        col.GetComponent<Collider2D>();
        
        character = player.GetComponent<DamageableCharacter>();
    }
    private void FixedUpdate()
    { 
        if(player.transform.childCount == 2)
        {
            gun = player.transform.GetChild(1).gameObject;
            if (gun != null)
            {
                if (isDanger == false)
                {
                    gun.GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    gun.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.gameObject.tag == tagTarget)
        {
            isDanger = true;
            detectedEnObjs.Add(collider);
            int countEnemies = detectedEnObjs.Count;
            if (countEnemies > 0)
            {
                playerAnimator.SetBool("isSafety", false);
                if(gun != null)
                {
                    gun.GetComponent<SpriteRenderer>().enabled = true;
                }
                

            }

        }
        if(col.gameObject.tag == "Dialogue") {
            character.SetPause();
        }
    }
    // Update is called once per frame
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagTarget)
        {
            detectedEnObjs.Remove(collider);
            int countEnemies = detectedEnObjs.Count;
            if (countEnemies == 0)
            {
                playerAnimator.SetBool("isSafety", true);
                if (gun != null)
                {

                    gun.GetComponent<SpriteRenderer>().enabled = false;
                }

                isDanger = false;
            }
            
        }
        if (col.gameObject.tag == "Dialogue")
        {
            character.UnPause();
        }
    }
}
