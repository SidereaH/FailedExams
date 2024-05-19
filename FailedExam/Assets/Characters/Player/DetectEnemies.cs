using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemies : MonoBehaviour
{
    public string tagTarget = "Enemy";
    public List<Collider2D> detectedEnObjs = new List<Collider2D>();
    public Animator playerAnimator;
    public Collider2D col;
    SpriteRenderer gunRenderer;
    public GameObject player;
    public bool isDanger = true;
    DamageableCharacter character;
    // Start is called before the first frame update
    void Start()
    {
        gunRenderer = player.transform.GetChild(0).GetComponent<SpriteRenderer>();
        col.GetComponent<Collider2D>();
        gunRenderer.enabled = false;
        character = player.GetComponent<DamageableCharacter>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(isDanger == true)
        {
            playerAnimator.SetBool("isSafety", false);
            gunRenderer.enabled = true;
        }
        else if (collider.gameObject.tag == tagTarget)
        {
            detectedEnObjs.Add(collider);
            int countEnemies = detectedEnObjs.Count;
            if (countEnemies > 0)
            {
                playerAnimator.SetBool("isSafety", false);
                gunRenderer.enabled = true;

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
                gunRenderer.enabled = false;
            }
            
        }
        if (col.gameObject.tag == "Dialogue")
        {
            character.UnPause();
        }
    }
}
