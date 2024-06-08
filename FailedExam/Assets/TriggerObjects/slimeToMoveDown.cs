using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeToMoveDown : MonoBehaviour
{
    
    public string tagTarget = "Player";
    [SerializeField] float maxSpeed;
    [SerializeField] float moveSpeed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            other.transform.GetComponent<Player>().getSlowed(maxSpeed,moveSpeed);
   
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.GetComponent<Player>().getUnSlowed();

        }
    }
}
