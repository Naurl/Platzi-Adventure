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

    private Camera theCamera;
    private BoxCollider2D cameraLimits;
    private Vector3 minLimits, maxLimits;
    private float halfWidth, halfHeight;

    private void Start()
    {
        cameraLimits = GetComponentInChildren<BoxCollider2D>();
        minLimits = cameraLimits.bounds.min;
        maxLimits = cameraLimits.bounds.max;

        theCamera = GetComponent<Camera>();
        halfWidth = theCamera.orthographicSize;
        halfHeight = halfWidth / Screen.width * Screen.height;

        if (!followTarget)
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

        float clampX = Mathf.Clamp(this.transform.position.x, minLimits.x + halfWidth, maxLimits.x - halfWidth);//Evita que x se pase de los valores min x + la mita del ancho de la pantalla y el max x - la mitad del ancho de la pantalla.
        float clampY = Mathf.Clamp(this.transform.position.y, minLimits.y + halfHeight, maxLimits.y - halfHeight);//Lo mismo que lo anterior pero con respecto a la altura de la pantalla.

        this.transform.position = new Vector3(clampX, clampY, this.transform.position.z);
    }
}
