using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healingBottle : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float healHealth = 1.0f;
    public DamageableCharacter player;
    
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damagableObject = (IDamageable)other.GetComponent<IDamageable>();
        if (damagableObject != null)
        {    
                damagableObject.OnHeal(healHealth, gameObject);
        }
    }
}
