using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaEnemy : MonoBehaviour
{
    public float velocity;
    public float jump;

    private Vector2 attackPosition;

    public Transform floorVeirfy;
    public Transform handPivot;

    public bool onFloor;

    public GameObject shuriken;
    public GameObject enemyHand;

    public GameObject spot1;
    public GameObject spot2;

    private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        sprite.flipX = true;
        handPivot.transform.eulerAngles = new Vector2(0.0f, 180.0f);

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

        //if(distance< attackLimit && !attack)
        //{
        //    jump = 10.0f;
        //    rigidbody.AddForce(Vector2.up * jump);

        //    if(onFloor == true)
        //    {
        //        attack = true;
        //    }
        //}

        //if (attack == true)
        //{

        //    attack = false;
        //}

        //StartCoroutine(EnemyMovement());
    }

    IEnumerator EnemyMovement()
    {
        int positionId = 1;

        if (onFloor = true)
        {
            yield return new WaitForSeconds(1.5f);
            jump = 200.0f;
            rigidbody.AddForce(Vector2.up * jump);
        }

        yield return new WaitForSeconds(1.5f);
        Instantiate(shuriken, enemyHand.transform.position, transform.rotation);

        yield return new WaitForSeconds(1.5f);
        if (positionId == 1)
        {
            attackPosition = spot2.transform.position;
            for (int i = 0; i < 60; i++)
            {
                yield return new WaitForSeconds(0.01f);
                transform.position = Vector2.MoveTowards(transform.position, spot2.transform.position, velocity * Time.deltaTime);
            }

            yield return new WaitForSeconds(1.5f);
            positionId++;
        }
    }
}
