using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Transform gameManager;

    bool alive = true;
    [SerializeField]
    float limit;
    [SerializeField]
    float movingSpeed;

    // Update is called once per frame
    void Update()
    {
        alive = gameManager.GetComponent<GameManager>().alive;
        if (alive)
        {
            if (transform.position.x > limit)
            {
                transform.position = transform.position + new Vector3(-movingSpeed * Time.deltaTime, 0);
            }
            else
            {
                Destroy(transform.gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.tag == "Player")
        {
            if (collider.GetComponent<Rigidbody2D>().gravityScale == 0)
            {
                Destroy(transform.gameObject);
            }
            else
            {
                gameManager.GetComponent<GameManager>().alive = false;
            }
        }
    }
}
