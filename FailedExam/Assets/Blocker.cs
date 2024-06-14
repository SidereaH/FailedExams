using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    [SerializeField] bool opened;
    [SerializeField] Opener open;
    void Start()
    {
        
    }

    public void FixedUpdate()
    {
        if (open.getOpen() == true) 
        {
            opened = true;
            transform.GetComponent<Collider2D>().enabled = false;
        }
    }
}
