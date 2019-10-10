using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{

    public float velocity;
    public float destroyLimit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * velocity * Time.deltaTime);

        if(transform.position.x >= destroyLimit)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        Destroy(gameObject);
    }
}
