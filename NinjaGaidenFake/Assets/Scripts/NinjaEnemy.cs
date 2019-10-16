﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaEnemy : MonoBehaviour
{
    public float velocity;
    public float jump;

    private int life = 3;

    public Transform floorVeirfy;
    public Transform enemy;

    public bool onFloor;   

    public GameObject shuriken;
    public GameObject enemyHand;

    public GameObject spot1;
    public GameObject spot2;

    private Vector2 attackPosition;

    private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        //handPivot.transform.eulerAngles = new Vector2(0.0f, 180.0f);
        enemy.transform.eulerAngles = new Vector2(0.0f, 180.0f);

        StartCoroutine(EnemyMovement());
    }

    // Update is called once per frame
    void Update()
    {
        //Player animation
        animator.SetBool("pJump", onFloor);
        animator.SetFloat("pMove", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

        //verify if is on floor
        onFloor = Physics2D.Linecast(transform.position, floorVeirfy.transform.position,
            1 << LayerMask.NameToLayer("Floor"));  
        
        if(life == 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator EnemyMovement()
    {
        int positionId = 1;

        while (true)
        {
            if (onFloor == true)
            {
                jump = 200.0f;
                for (int i=0; i < 3; i++)
                {
                    yield return new WaitForSeconds(1.0f);
                    rigidbody.AddForce(Vector2.up * jump);
                }                
            }

            yield return new WaitForSeconds(1.5f);

            if (positionId == 1)
            {
                Instantiate(shuriken, enemyHand.transform.position, transform.rotation);
                yield return new WaitForSeconds(1.5f);

                attackPosition = spot2.transform.position;
                for (int i = 0; i <= 80; i++)
                {
                    yield return new WaitForSeconds(0.01f);
                    transform.position = Vector2.MoveTowards(transform.position, attackPosition,
                        velocity * Time.deltaTime);
                }
                //sprite.flipX = true;
                enemy.transform.eulerAngles = new Vector2(0.0f, 0.0f);
                positionId++;
            }
            else if (positionId == 2)
            {
                Instantiate(shuriken, enemyHand.transform.position, transform.rotation);
                yield return new WaitForSeconds(1.5f);

                attackPosition = spot1.transform.position;
                for (int i = 0; i <= 80; i++)
                {
                    yield return new WaitForSeconds(0.01f);
                    transform.position = Vector2.MoveTowards(transform.position, attackPosition,
                        velocity * Time.deltaTime);
                }
                //sprite.flipX = false;
                enemy.transform.eulerAngles = new Vector2(0.0f, 180.0f);
                positionId = 1;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.tag == "Shuriken")
        {
            StartCoroutine(TookDamage());
        }
    }

    IEnumerator TookDamage()
    {
        life--;
        for (int i=0; i<3; i++)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
