using UnityEngine;

//The player class extends the LivingEntity class which allows it access to health variables and damage functions
public class Player : LivingEntity
{
    public LayerMask gunMask;
    float distance = 20f;
    GunController gunController;

    AudioSource shootSound;
    
    protected override void Start()
    {
        base.Start();
        gunController = GetComponent<GunController>();
        shootSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if ((Input.GetMouseButton(0) || Input.GetMouseButtonDown(0)) && gunController.gunToBeEquipped == true)
        {
            shootSound.Play();
            gunController.Shoot();
        }
    }
}
