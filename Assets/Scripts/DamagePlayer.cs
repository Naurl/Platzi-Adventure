using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePlayer : MonoBehaviour
{
    public float timeToRevivePlayer;
    private float timeRevivalCounter;

    private bool playerReviving;

    private GameObject thePlayer = null;

    public int damage;
    public GameObject damageNumber;


    private void Start()
    {
        if(thePlayer == null)
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerReviving)
        {
            timeRevivalCounter -= Time.deltaTime;

            if(timeRevivalCounter < 0)
            {
                playerReviving = false;
                //thePlayer.GetComponent<HealthManager>().currentHealth = thePlayer.GetComponent<HealthManager>().maxHealth;
                thePlayer.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//Cuando la colicion no es en forma de trigger hay que usar los metodos OnCollision
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Colicion entre colicion box de daño con el jugador
            //collision.gameObject.SetActive(false);
            //playerReviving = true;
            //timeRevivalCounter = timeToRevivePlayer;
            //thePlayer = collision.gameObject;//Me aseguro de obtener el ultimo objeto que representa al jugador.

            //collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);

            CharacterStats stats = collision.gameObject.GetComponent<CharacterStats>();
            int totalDamage = damage - stats.defenseLevels[stats.currentLevel];

            if(totalDamage <= 0)
            {
                totalDamage = 1;
            }

            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);

            GameObject damageNumberClone = (GameObject)Instantiate(damageNumber, collision.gameObject.transform.position, Quaternion.Euler(Vector3.zero));//Quaternion.Euler(Vector3.zero) reinicio la rotacion del objeto que instanciamos para no tomar la rotacion del hitpoint.

            damageNumberClone.GetComponent<DamageNumber>().damagePoints = totalDamage;
            damageNumberClone.GetComponent<DamageNumber>().damageText = damageNumberClone.GetComponentInChildren<Text>();
            damageNumberClone.GetComponent<DamageNumber>().damageText.color = new Color(255f, 0f, 0f);
            //playerReviving = true;
            //timeRevivalCounter = timeToRevivePlayer;
            //thePlayer = collision.gameObject;//Me aseguro de obtener el ultimo objeto que representa al jugador.
        }
    }
}
