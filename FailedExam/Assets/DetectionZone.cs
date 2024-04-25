using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public string tagTarget = "Player";
    public List<Collider2D> detectedObjs = new List<Collider2D>();
    public Animator playerAnimator;
    public Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col.GetComponent<Collider2D>();
    }
    //чужой коллайдер вошел в зону
    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if(collider.gameObject.tag == tagTarget)
        {
            detectedObjs.Add(collider);
            playerAnimator.SetBool("isSafety", false);

        }
    }
    //вышел из зоны
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagTarget)
        {
            detectedObjs.Remove(collider);

        }
    }

}
