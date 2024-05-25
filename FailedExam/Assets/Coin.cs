using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int goldCost;
    Collider2D col;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        col = transform.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            
            player = collision.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("+gold");
                player.addGold(goldCost);
                Destroy(gameObject);
            }

        }
    }
}
