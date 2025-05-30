using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heroe : MonoBehaviour
{
    float speed = 5.0f;
    float angle = 5.0f;

    void Start()
    {

    }

    void Update()
    {
        float horizontal = Input.GetAxis( "Horizontal" );
        float vertical = Input.GetAxis( "Vertical" );

        transform.Rotate( 0, horizontal*angle, 0, Space.Self );

        Vector3 moveDirection = new Vector3( 0, 0, vertical );
        moveDirection = transform.rotation * moveDirection;
        transform.position += ( moveDirection * speed * Time.deltaTime );
    }
}
