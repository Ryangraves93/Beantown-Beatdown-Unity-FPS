using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    GunController gunController;
    // Start is called before the first frame update
    void Start()
    {
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
