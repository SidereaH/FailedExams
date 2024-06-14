using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opener : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool canOpen = false;
    public void Open()
    {
        canOpen = true;
    }
    public bool getOpen()
    {
        return canOpen;
    }
}
