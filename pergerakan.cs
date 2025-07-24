using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pergerakan : MonoBehaviour
{
    private Vector3 move;
    public float speed = 4.0f;
    private Animator animasi;


    void Start()
    {
        
    }

    void Update()
    {
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        transform.position += move * speed * Time.deltaTime;

        if (move != Vector3.zero)
        {
           animasi.SetBool("isMoving", true);
        }
        else
        {
            animasi.SetBool("isMoving", false);
        }

        if (move == Vector3.left)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (move == Vector3.right)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
