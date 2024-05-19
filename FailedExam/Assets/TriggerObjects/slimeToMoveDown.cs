using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeToMoveDown : MonoBehaviour
{
    
    public string tagTarget = "Player";
    public Player player;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            player.getSlowed();
   
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.getUnSlowed();

        }
    }
}
