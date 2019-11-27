using UnityEngine;

public class GunRotate : MonoBehaviour
{

    float speed = 50.0f;
   
    void Update()//Rotates weapons in shop by the speed variable
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
