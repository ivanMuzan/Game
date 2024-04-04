using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarAttack : MonoBehaviour
{
    [NonSerialized]
    
    public int _health = 100;
    public float radius = 70f;
    public GameObject bullet;
    private Coroutine _coroutine = null;

    private void Update()
    {
        DetectCollision();
    }

    private void DetectCollision()
    {
        Collider[] hittColliders = Physics.OverlapSphere(transform.position, radius);

        if(hittColliders.Length == 0 && _coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            if(gameObject.CompareTag("Enemy"))
            {
                GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
            }
        }

        foreach (var el  in hittColliders)
        {
            if(
                //!gameObject.CompareTag(el.gameObject.tag))
                (gameObject.CompareTag("Player") && el.gameObject.CompareTag("Enemy"))||
                (el.gameObject.CompareTag("Player") && gameObject.CompareTag("Enemy"))
                )
            {
                if(gameObject.CompareTag("Enemy"))
                    GetComponent<NavMeshAgent>().SetDestination(el.transform.position);

                if(_coroutine == null)
                    _coroutine = StartCoroutine(StartAttack(el));
            }
        }
    }

    IEnumerator StartAttack(Collider enemyPos)
    {
        GameObject obj = Instantiate(bullet, transform.GetChild(1).transform.position, Quaternion.identity);
        obj.GetComponent<BulletController>().position = enemyPos.transform.position;
        yield return new WaitForSeconds(1f);
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}
