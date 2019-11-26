using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Deals with camera motion for the main menu
public class CameraRotator : MonoBehaviour
{

    public float speed;

    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
