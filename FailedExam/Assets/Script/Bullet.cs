using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
    
{
    private GameObject Player;
    Rigidbody2D rb;
    public float speed, timer;

    // Start is called before the first frame update
    void Start()
    {
        name = "Bullet";
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        Vector2 direction = (Vector2.up * (Player.transform.position.y - transform.position.y) + Vector2.right * (Player.transform.position.x - transform.position.x)) / 
            (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(Player.transform.position.x, Player.transform.position.y)) / 3);
        rb.AddForce(direction * speed * Time.deltaTime, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);

        }
    }
    private void OnTriggerEnter(Collider other)
    {   
        if (other.name == "Player")
        {
            Destroy(gameObject);
        }
    }
}
