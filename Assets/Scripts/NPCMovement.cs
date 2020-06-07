using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float enemySpeed;
    private Rigidbody2D enemyRigidBody;

    private bool isMoving = false;

    public float timeBetweenSteps;
    private float timeBetweenStepsCounter;

    public float timeToMakeStep;
    private float timeToMakeStepCounter;

    public Vector2 directionToMakeStep;

    private Animator enemyAnimator;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";

    public BoxCollider2D NPC_zone;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();

        timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0.5f, 1.5f);//Cuando los parametros de range son float, se incluyen los extremos.
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if(NPC_zone != null)
            {
                if(this.transform.position.x < NPC_zone.bounds.min.x ||
                   this.transform.position.x > NPC_zone.bounds.max.x ||
                   this.transform.position.y < NPC_zone.bounds.min.y ||
                   this.transform.position.y > NPC_zone.bounds.max.y)
                {
                    //StopWalking();

                    // Obtiene un vector que apunta desde la posición del npc a un punto aleatorio dentro de los limites de la zona del NPC.
                    directionToMakeStep = (new Vector3(Random.Range(NPC_zone.bounds.min.x, NPC_zone.bounds.max.x), Random.Range(NPC_zone.bounds.min.y, NPC_zone.bounds.max.y), NPC_zone.bounds.min.z) - this.transform.position).normalized * enemySpeed;
                }
            }

            timeToMakeStepCounter -= Time.deltaTime;
            enemyRigidBody.velocity = directionToMakeStep;

            if (timeToMakeStepCounter < 0)
            {
                StopWalking();
            }
        }
        else
        {
            timeBetweenStepsCounter -= Time.deltaTime;
            if (timeBetweenStepsCounter < 0)
            {
                isMoving = true;
                timeToMakeStepCounter = timeToMakeStep;
                directionToMakeStep = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * enemySpeed;//El range no incluye los valores extremos cuando son numeros enteros.
            }
        }

        enemyAnimator.SetFloat(horizontal, directionToMakeStep.x);
        enemyAnimator.SetFloat(vertical, directionToMakeStep.y);

    }

    private void StopWalking()
    {
        isMoving = false;
        timeBetweenStepsCounter = timeBetweenSteps;
        enemyRigidBody.velocity = Vector2.zero;
    }
}
