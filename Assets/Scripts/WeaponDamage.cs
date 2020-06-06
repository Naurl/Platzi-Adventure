using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int damage;

    public GameObject hurtAnimation;
    public GameObject HitPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);

            Instantiate(hurtAnimation, HitPoint.transform.position, HitPoint.transform.rotation);
        }
    }
}
