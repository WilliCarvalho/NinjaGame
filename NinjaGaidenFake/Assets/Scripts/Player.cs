using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject player;
    public Transform floorVeirfy;
    public Transform handPivot;

    public float velocity;
    public float jump;

    public int life;

    public bool onFloor;

    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;

    public Text lifeText;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        life = 3;
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
        if (moveX > 0)
        {
            sr.flipX = false;
            handPivot.transform.eulerAngles = new Vector2(0.0f, 0.0f);
        }
        else if (moveX < 0)
        {
            sr.flipX = true;
            handPivot.transform.eulerAngles = new Vector2(0.0f, 180.0f);
        }

        //Player animation
        anim.SetBool("pJump", onFloor);
        anim.SetFloat("pMove", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

        //life verificator
        lifeText.text = "Lifes:" + life.ToString();
        if (life == 0)
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Enemy" || c.gameObject.tag == "Shuriken")
        {
            StartCoroutine(TookDamage());
        }
    }

    IEnumerator TookDamage()
    {
        life--;

        for (int i = 0; i < 3; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
