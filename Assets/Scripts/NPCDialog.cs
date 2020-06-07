using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour
{

    public string[] dialog;
    private DialogManager manager;
    private bool playerInTheZone;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<DialogManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerInTheZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerInTheZone = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInTheZone && Input.GetAxis("Submit") > 0)
        {
            manager.ShowDialog(dialog);
        }
    }
}
