using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4.0f;
    //private float currentSpeed;
    private bool isWalking = false;
    public Vector2 lastMovement = Vector2.zero;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string animp_isWalking = "IsWalking";
    private const string animp_LastHorizontal = "LastHorizontal";
    private const string animp_LastVertical = "LastVertical";
    private const string animp_isAttacking = "IsAttacking";

    private Animator animator;
    private Rigidbody2D playerRigidBody;

    public static bool playerCreated;

    public string nextPlaceName;

    private bool isAttacking = false;
    public float attackTime;
    private float attackTimeCounter;

    private GameObject HitPoint = null;

    private SFXManager sfxManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        HitPoint = GameObject.FindGameObjectWithTag("HitPoint");
        sfxManager = FindObjectOfType<SFXManager>();

        if (!playerCreated)
        {
            playerCreated = true;
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            Destroy(this.transform.gameObject);
        }

        lastMovement = new Vector2(1f, 0f);//Set dafault lastMovement on start to avoid animator bug.
    }

    // Update is called once per frame
    void Update()
    {
        // speed = velocity*time;
        isWalking = false;

        //if(Input.GetMouseButtonDown(0)) Click Izquierdo del raton
        if (Input.GetAxis("Fire1") > 0)
        {
            isAttacking = true;
            attackTimeCounter = attackTime;
            //playerRigidBody.velocity = Vector2.zero;
            animator.SetBool(animp_isAttacking, true);
            sfxManager.playerAttack.Play();

            if (HitPoint != null)
            {
                PolygonCollider2D collider = HitPoint.GetComponent<PolygonCollider2D>();

                collider.enabled = true;
            }
        }

        if(isAttacking)
        {
            attackTimeCounter -= Time.deltaTime;
            if(attackTimeCounter < 0)
            {
                isAttacking = false;
                animator.SetBool(animp_isAttacking, false);

                if(HitPoint != null)
                {
                    PolygonCollider2D collider = HitPoint.GetComponent<PolygonCollider2D>();

                    collider.enabled = false;
                }
            }
        }
        
        //Optimizacion del movimiento.
        if(Math.Abs(Input.GetAxisRaw(horizontal)) > 0.5f ||
            Math.Abs(Input.GetAxisRaw(vertical)) > 0.5f
            )
        {
            isWalking = true;
            lastMovement = new Vector2(Input.GetAxisRaw(horizontal), Input.GetAxisRaw(vertical));
            playerRigidBody.velocity = lastMovement.normalized * speed;//Normalized normaliza el vector de movimiento en caso de producirse el movimiento vertical y horizontal a la vez.
        }

        /* Codido desoptimizado
        if (Math.Abs(Input.GetAxisRaw(horizontal)) > 0.5f)
        {
            //this.transform.Translate(new Vector3(Input.GetAxisRaw(horizontal) * speed * Time.deltaTime,0,0));
            playerRigidBody.velocity = new Vector2(Input.GetAxisRaw(horizontal) * currentSpeed, playerRigidBody.velocity.y);//Movimiento recomendado cuando se hace uso del motor de fisicas con cuerpos rigidos.//Para este movimiento de cuerpos rigidos no es necesario multiplicar el delta time
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
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, Input.GetAxisRaw(vertical) * currentSpeed);//Movimiento recomendado cuando se hace uso del motor de fisicas con cuerpos rigidos.//Para este movimiento no es necesario multiplicar el delta time
            isWalking = true;
            lastMovement = new Vector2(0, Input.GetAxisRaw(vertical));
        }
        else
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0f);
        }
        

        //Si ambos movimientos se producen al mismo tiempo, hay que dividir el vector por su hipotenuza en este caso raiz de 2 debido a que cada uno de los catetos tiene una longitud maxima de 1.
        if(Math.Abs(Input.GetAxisRaw(horizontal)) > 0.5f && Math.Abs(Input.GetAxisRaw(vertical)) > 0.5f)
        {
            currentSpeed = speed / Mathf.Sqrt(2);
        }
        else
        {
            currentSpeed = speed;
        }
        */


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
