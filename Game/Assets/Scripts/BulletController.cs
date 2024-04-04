using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [NonSerialized]
    public Vector3 position;
    public float speed = 30f;
    public int damage = 20;

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);

        if(transform.position == position )
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            CarAttack attack = other.GetComponent<CarAttack>();
            attack._health -= damage;

            Transform healthbar = other.transform.GetChild(0).transform;
            healthbar.localScale = new Vector3(
                healthbar.localScale.x - 0.3f,
                healthbar.localScale.y,
                healthbar.localScale.z);

            if(attack._health <= 0 )
                Destroy(other.gameObject);
        }
    }
}
