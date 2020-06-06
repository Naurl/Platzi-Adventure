using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNewPlace : MonoBehaviour
{
    public string newPlaceName = "New Scene Name Here";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag.Equals("Player"))// Camparacion ineficiente
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(newPlaceName);
        }
    }
}
