using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int daño = 1;

    public float velocidad = 4.0f;
    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direccion = (player.position - transform.position).normalized;
            transform.position += direccion * velocidad * Time.deltaTime;
        }
    }
}
