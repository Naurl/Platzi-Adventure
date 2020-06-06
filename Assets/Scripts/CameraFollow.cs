using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public string tagTarget = "Player";

    [SerializeField]
    private GameObject followTarget;

    [SerializeField]
    private Vector3 targetPosition;
    [SerializeField]
    private float cameraSpeed = 4.0f;

    private void Start()
    {
        if(!followTarget)
        {
            followTarget = GameObject.FindGameObjectWithTag(tagTarget);
        }
    }
    // Update is called once per frame
    void Update()
    {
        targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, this.transform.position.z);

        //Interpolacion lineal: Movimiento paulatino entre 2 puntos en el tiempo.
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * cameraSpeed);
    }
}
