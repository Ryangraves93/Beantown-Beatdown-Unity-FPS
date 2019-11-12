using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{

    GunController gunController;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        gunController = GetComponent<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }
}
