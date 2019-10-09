using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaEnemy : MonoBehaviour
{
    public float velocity;
    public float attackLimit;
    public float jump;

    public Transform floorVeirfy;
    public Transform handPivot;

    public bool onFloor;

    public GameObject target;
    public GameObject shuriken;
    public GameObject hand;

    public GameObject spot1;
    public GameObject spot2;

    private Vector2 attackPosition;

    private bool attack = false;

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

        float distance = Vector2.Distance(transform.position, target.transform.position);

        if(distance< attackLimit && !attack)
        {
            jump = 10.0f;
            rigidbody.AddForce(Vector2.up * jump);
            
            if(onFloor == true)
            {
                attack = true;
            }
        }

        if (attack == true)
        {
            Instantiate(shuriken, hand.transform.position, transform.rotation);
            attack = false;
            StartCoroutine(ChangePosition());
        }
    }

    IEnumerator ChangePosition()
    {
        int spotId = 1;

        yield return new WaitForSeconds(1.5f);

        if(spotId == 1)
        {
            //transform.Translate(spot2.transform.position);
        }
    }
}
