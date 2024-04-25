using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemies : MonoBehaviour
{
    public string tagTarget = "Enemy";
    public List<Collider2D> detectedEnObjs = new List<Collider2D>();
    public Animator playerAnimator;
    public Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col.GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag == tagTarget)
        {
            detectedEnObjs.Add(collider);
            int countEnemies = detectedEnObjs.Count;
            if (countEnemies > 0)
            {
                playerAnimator.SetBool("isSafety", false);
            }

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
            }
            
        }
    }
}
