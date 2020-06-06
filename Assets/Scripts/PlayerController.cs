using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4.0f;
    private bool isWalking = false;
    public Vector2 lastMovement = Vector2.zero;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string animp_isWalking = "IsWalking";
    private const string animp_LastHorizontal = "LastHorizontal";
    private const string animp_LastVertical = "LastVertical";

    private Animator animator;
    private Rigidbody2D playerRigidBody;

    public static bool playerCreated;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();

        if(!playerCreated)
        {
            playerCreated = true;
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // speed = velocity*time;
        isWalking = false;

        if (Math.Abs(Input.GetAxisRaw(horizontal)) > 0.5f)
        {
            //this.transform.Translate(new Vector3(Input.GetAxisRaw(horizontal) * speed * Time.deltaTime,0,0));
            playerRigidBody.velocity = new Vector2(Input.GetAxisRaw(horizontal) * speed * Time.deltaTime, playerRigidBody.velocity.y);//Movimiento recomendado cuando se hace uso del motor de fisicas con cuerpos rigidos.
            isWalking = true;
            lastMovement = new Vector2(Input.GetAxisRaw(horizontal), 0);
        }
        else
        {
            playerRigidBody.velocity = new Vector2(0f, playerRigidBody.velocity.y);
        }

        if (Math.Abs(Input.GetAxisRaw(vertical)) > 0.5f)
        {
            //this.transform.Translate(new Vector3(0, Input.GetAxisRaw(vertical) * speed * Time.deltaTime, 0));
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, Input.GetAxisRaw(vertical) * speed * Time.deltaTime);//Movimiento recomendado cuando se hace uso del motor de fisicas con cuerpos rigidos.
            isWalking = true;
            lastMovement = new Vector2(0, Input.GetAxisRaw(vertical));
        }
        else
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0f);
        }

        //Al usar cuerpos rigidos los cuerpos luego de moverse generan cierta inercia, pero en nuestro caso queremos que pare de golpe.
        if(!isWalking)
        {
            playerRigidBody.velocity = Vector2.zero;
        }

        animator.SetBool(animp_isWalking, isWalking);
        animator.SetFloat(animp_LastHorizontal, lastMovement.x);
        animator.SetFloat(animp_LastVertical, lastMovement.y);
        animator.SetFloat(horizontal, Input.GetAxisRaw(horizontal));
        animator.SetFloat(vertical, Input.GetAxisRaw(vertical));
    }
}
