using UnityEngine;

//LivingEntity extends to the IDamageable interface which uses both TakeHit and TakeDamage functions
public class LivingEntity : MonoBehaviour, IDamageable
{

    //Health Variables
    public float startingHealth;
    public float health;
    int scoreFromKill = 1;

    public bool dead;
    
    public static int score = 0;

    public event System.Action OnDeath;

   
    protected virtual void Start ()//Sets starting health to startingHealth which is set in the inspector
    {
        health = startingHealth;
    }
    public void Update()
    {
      
    }
    //Passes in the damage variable into the TakeDamage method
    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection) {
        TakeDamage(damage);
    }
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
          
            Die(scoreFromKill);
        }
    }
    protected void Die(int newScore)//Method responsible for destroying gameObjects
    {
        dead = true;
        
        if (OnDeath != null)
        {
            OnDeath();
        }
        if (FindObjectOfType<Player>().dead)
        {
            transform.DetachChildren();//Neccesary so the camera does not get destory along with the gameObject

        }
        GameObject.Destroy(gameObject);
        score+=newScore;
       
       
       
    }
}
