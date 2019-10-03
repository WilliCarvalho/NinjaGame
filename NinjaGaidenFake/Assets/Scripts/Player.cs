﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    public Transform floorVeirfy;

    public float velocity;
    public float jump;

    public bool onFloor;

    SpriteRenderer sr;
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        //player move
        float moveX = Input.GetAxisRaw("Horizontal") * velocity * Time.deltaTime;
        transform.Translate(moveX, 0.0f, 0.0f);

        //verify if is on floor
        onFloor = Physics2D.Linecast(transform.position, floorVeirfy.transform.position,
            1 << LayerMask.NameToLayer("Floor"));
        
        //jump
        if (Input.GetButtonDown("Jump") && onFloor == true)
        {
            rb.AddForce(Vector2.up * jump);
        }

        //player orientation
        if(moveX > 0)
        {
            sr.flipX = false;
        }
        else if (moveX < 0)
        {
            sr.flipX = true;
        }
    }
}
