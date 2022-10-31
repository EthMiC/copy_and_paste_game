using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;

    [SerializeField]
    Transform gameManager;

    bool alive = true;

    bool jumping;
    bool grounded;
    [SerializeField]
    int dashStart;
    int dashLeft;
    [SerializeField]
    float jumpSpeed;
    [SerializeField]
    float dashSpeed;
    [SerializeField]
    float deathForce;
    [SerializeField]
    float dashTimer;
    [SerializeField]
    float xPos;
    [SerializeField]
    float initTime;
    float time;
    float currentXPos;
    bool pushed;

    // Start is called before the first frame update
    void Start()
    {
        dashLeft = dashStart;
        pushed = false;
    }

    // Update is called once per frame
    void Update()
    {
        alive = gameManager.GetComponent<GameManager>().alive;
        if (alive)
        {
            if (Input.GetKeyDown("space") && jumping == false)
            {
                if (grounded)
                {
                    jumping = true;
                }
            }
            if (jumping)
            {
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                jumping = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                DashToPoint();
                StartCoroutine(Jump(rb));
            }
            else if (transform.position.x != xPos && rb.gravityScale == 1)
            {
                transform.position = new Vector3(Mathf.Lerp(xPos, currentXPos, Mathf.Cos((Mathf.PI * (time / initTime)) / 2)), transform.position.y, 1);
                time += Time.deltaTime;
            }
            else if (transform.position.x == xPos && rb.gravityScale == 1)
            {
                time = 0;
                Vector3 vel = rb.velocity;
                vel.x = 0f;
                rb.velocity = vel;
                transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
            }
        }
        else if (!pushed)
        {
            rb.AddForce(transform.right * deathForce, ForceMode2D.Impulse);
            pushed = true;
        }
    }
    void DashToPoint()
    {
        Vector3 initMousePos = Input.mousePosition;
        initMousePos.z = 11;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(initMousePos);
        Vector3 difference = mousePos - transform.position;
        difference = difference.normalized;
        rb.velocity = Vector3.zero;
        rb.AddForce(difference * dashSpeed, ForceMode2D.Impulse);
        time = 0;
    }
    IEnumerator Jump(Rigidbody2D rigidbody)
    {
        rigidbody.gravityScale = 0f;

        yield return new WaitForSeconds(dashTimer);

        rigidbody.gravityScale = 1;
        currentXPos = transform.position.x;
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            grounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            grounded = false;
        }
    }
}
