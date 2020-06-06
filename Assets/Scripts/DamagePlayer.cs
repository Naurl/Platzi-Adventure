using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public float timeToRevivePlayer;
    private float timeRevivalCounter;

    private bool playerReviving;

    private GameObject thePlayer = null;

    public int damage;


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

            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
            //playerReviving = true;
            //timeRevivalCounter = timeToRevivePlayer;
            //thePlayer = collision.gameObject;//Me aseguro de obtener el ultimo objeto que representa al jugador.
        }
    }
}
