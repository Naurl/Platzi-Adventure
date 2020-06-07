using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int damage;

    public GameObject hurtAnimation;
    public GameObject HitPoint;

    public GameObject damageNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);

            Instantiate(hurtAnimation, HitPoint.transform.position, HitPoint.transform.rotation);

            GameObject damageNumberClone = (GameObject)Instantiate(damageNumber, HitPoint.transform.position, Quaternion.Euler(Vector3.zero));//Quaternion.Euler(Vector3.zero) reinicio la rotacion del objeto que instanciamos para no tomar la rotacion del hitpoint.

            damageNumberClone.GetComponent<DamageNumber>().damagePoints = damage;
        }
    }
}
