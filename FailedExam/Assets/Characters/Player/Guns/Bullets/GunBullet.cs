using UnityEngine;

public class GunBullet : MonoBehaviour
{
    [SerializeField] BullType bullType;
    public float speed;
    public float lifetime;
    public int damage;
    public float distance;
    public LayerMask whatIsSolid;
    public float knockBackForce = 1f;
    [SerializeField] GameObject soundHeadshot;
    [SerializeField] GameObject soundBodyshot;
    Collider2D parentCol;
    [SerializeField] bool enemyBulet;
    Animator animator;
    [SerializeField] enum BullType { grenade, defaultgun }
    [SerializeField] GameObject grenadeObj;
    [SerializeField] GameObject[] brokegrenade;
    private void Start()
    {
        if (transform.GetComponent<Animator>() != null)
        {
            animator = transform.GetComponent<Animator>();
        }
    }
    private void Update()
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {

            if (enemyBulet == false)
            {
                parentCol = hitInfo.collider.GetComponentInParent<Collider2D>();
                if (parentCol.CompareTag("Enemy"))
                {

                    DamageableCharacter damagableObject = hitInfo.collider.GetComponentInParent<DamageableCharacter>();
                    if (damagableObject != null)

                    {


                        Vector3 parentPosition = transform.root.position;
                        Vector2 direction = (hitInfo.collider.transform.position - parentPosition).normalized;
                        Vector2 knockback = direction * knockBackForce;


                        if (hitInfo.collider.name == "Head")
                        {

                            //GameObject _temp = Instantiate(soundHeadshot, transform.position, Quaternion.identity);
                            damagableObject.OnHit(damage * 2, knockback);

                            //Destroy(_temp, 1);

                        }
                        else if (hitInfo.collider.name == "Body")
                        {
                            //GameObject _temp = Instantiate(soundBodyshot, transform.position, Quaternion.identity);
                            //Destroy(_temp, 2);

                            damagableObject.OnHit(damage * 1, knockback);

                        }
                        else
                        {

                            //GameObject _temp = Instantiate(soundBodyshot, transform.position, Quaternion.identity);
                            //Destroy(_temp, 2);

                            damagableObject.OnHit(damage * 0.5f, knockback);

                        }
                        Destroy(gameObject);
                    }
                }
            }
            if (enemyBulet == true)
            {

                DamageableCharacter damagableObject = hitInfo.collider.GetComponentInParent<DamageableCharacter>();
                if (damagableObject != null)
                {

                    if (bullType.ToString() == "defaultgun")
                    {
                        Vector3 parentPosition = transform.root.position;
                        Vector2 direction = (hitInfo.collider.transform.position - parentPosition).normalized;
                        Vector2 knockback = direction * knockBackForce;
                        if (animator != null)
                        {
                            animator.SetTrigger("Shoot");
                        }
                        damagableObject.OnHit(damage, knockback);
                    }
                    else
                    {//tut nado razbivat
                        if (brokegrenade != null)
                        {
                            GameObject instBoomSound = brokegrenade[UnityEngine.Random.Range(0, brokegrenade.Length)];
                            GameObject _temp = Instantiate(instBoomSound, transform.position, Quaternion.identity);
                            Destroy(_temp, 1);
                        }
                        Instantiate(grenadeObj, transform.position, Quaternion.identity);

                    }

                    /*
                     if (hitInfo.collider.name == "Head")
                     {

                         GameObject _temp = Instantiate(soundHeadshot, transform.position, Quaternion.identity);
                         damagableObject.OnHit(damage * 2, knockback);

                         Destroy(_temp, 1);

                     }
                     else if (hitInfo.collider.name == "Body")
                     {
                         //GameObject _temp = Instantiate(soundBodyshot, transform.position, Quaternion.identity);
                         //Destroy(_temp, 2);

                         damagableObject.OnHit(damage * 1, knockback);

                     }
                     else
                     {

                         //GameObject _temp = Instantiate(soundBodyshot, transform.position, Quaternion.identity);
                         //Destroy(_temp, 2);
                         damagableObject.OnHit(damage * 1f, knockback);

                     }
                    */

                }
                if (bullType.ToString() == "grenade")
                {
                    if (brokegrenade != null)
                    {
                        GameObject instBoomSound = brokegrenade[UnityEngine.Random.Range(0, brokegrenade.Length)];
                        GameObject _temp = Instantiate(instBoomSound, transform.position, Quaternion.identity);
                        Destroy(_temp, 1);
                    }
                    Instantiate(grenadeObj, transform.position, Quaternion.identity);
                }
                {

                }
            }
            Destroy(gameObject);


        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    public void ShotBullet()
    {

    }
    public void destroyBullet()
    {
        Destroy(gameObject);
    }
}
