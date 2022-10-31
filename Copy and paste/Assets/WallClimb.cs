using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimb : MonoBehaviour
{

    [SerializeField]
    Transform gameManager;

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
                transform.position = new Vector3(-limit, 0);
                transform.position = transform.position + new Vector3(-movingSpeed * Time.deltaTime, 0);
            }
        }
    }
}
