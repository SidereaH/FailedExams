using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public float timeToLive = 0.5f;
    public float speed = 50f;
    public TextMeshProUGUI textMesh;
    float timeElapsed = 0.0f;
    RectTransform rectTransform;
    public Vector3 floatDirection = new Vector3 (0, 1, 0);
    // Start is called before the first frame update
    Color startColor;
    void Start()
    {
        
        startColor = textMesh.color;
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        rectTransform.position += floatDirection * speed * Time.deltaTime;
        textMesh.color = new Color(startColor.r, startColor.g, startColor.b, 1-(timeElapsed/timeToLive));
        if (timeElapsed > timeToLive)
        {
            Destroy(gameObject);
        }
    }
}
