using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpScore : MonoBehaviour
{
    public Vector2 initialVelocity;
    public Rigidbody2D rb;
    public float lifeTime=1f;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = initialVelocity;
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
