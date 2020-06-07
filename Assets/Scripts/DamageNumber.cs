using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    public float damageSpeed = 1f;
    public float damagePoints;

    public Text damageText;

    // Start is called before the first frame update
    void Start()
    {
        damageText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        damageText.text = damagePoints.ToString();
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + damageSpeed * Time.deltaTime, this.transform.position.z);
    }
}
