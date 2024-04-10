using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedObjs = new List<Collider2D>();

    public Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col.GetComponent<Collider2D>();
    }
    //чужой коллайдер вошел в зону
    private void OnTriggerEnter2D(Collider2D collider)
    {
        detectedObjs.Add(collider);
    }
    //вышел из зоны
    private void OnTriggerExit2D(Collider2D collider)
    {
        detectedObjs.Remove(collider);  
    }

}
