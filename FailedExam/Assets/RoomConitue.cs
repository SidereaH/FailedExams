using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class RoomConitue : MonoBehaviour
{
    
    public string tagTarget = "Enemy";
    public List<Collider2D> detectedEnObjs = new List<Collider2D>();
    Opener open;
    [SerializeField] Collider2D startBlock;
    private void Start()
    {
        open = GetComponent<Opener>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag== "Player")
        {
            startBlock.enabled = true;
            Debug.Log("Enabled");
        }
        if (collider.gameObject.tag == tagTarget)
        {
            
            detectedEnObjs.Add(collider);
            int countEnemies = detectedEnObjs.Count;
            if (countEnemies > 0)
            {

                
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
                open.Open();
            }

        }
    }


}
