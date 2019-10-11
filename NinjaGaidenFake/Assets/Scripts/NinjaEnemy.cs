using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaEnemy : MonoBehaviour
{
    public float velocity;
    public float jump;

    public Transform floorVeirfy;
    public Transform handPivot;

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

        while (true)
        {


            if (onFloor = true)
            {
                yield return new WaitForSeconds(1.5f);
                jump = 200.0f;
                rigidbody.AddForce(Vector2.up * jump);
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
                sprite.flipX = false;
                handPivot.transform.eulerAngles = new Vector2(0.0f, 0.0f);
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
                sprite.flipX = true;
                handPivot.transform.eulerAngles = new Vector2(0.0f, 180.0f);
                positionId = 1;
            }
        }
    }
}
