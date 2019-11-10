using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    float speed = 10;
    Collider col;

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
     
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponentInChildren<Enemy>().TakeDamage(1f,collision.transform.position,Vector3.forward); 
    }

}
