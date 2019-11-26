using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The player class extends the LivingEntity class which allows it access to health variables and damage functions
public class Player : LivingEntity
{
    public LayerMask gunMask;
    float distance = 20f;
    GunController gunController;

    AudioSource shootSound;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        gunController = GetComponent<GunController>();
        shootSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //shootSound.Play();
            gunController.Shoot();
        }
        RaycastHit hit;
       Physics.Raycast(transform.position, transform.forward,out hit,distance, gunMask);
        {
            //Debug.Log(hit.distance + hit.collider.ToString());
        }
    }
}
