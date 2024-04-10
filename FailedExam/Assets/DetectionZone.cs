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
    //����� ��������� ����� � ����
    private void OnTriggerEnter2D(Collider2D collider)
    {
        detectedObjs.Add(collider);
    }
    //����� �� ����
    private void OnTriggerExit2D(Collider2D collider)
    {
        detectedObjs.Remove(collider);  
    }

}
