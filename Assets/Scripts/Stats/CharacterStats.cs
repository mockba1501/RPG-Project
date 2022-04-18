using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    //Property any class can get the value, but we only can set its value from within this class
    public int currentHealth { get; private set; }
    public Stats damage;
    public Stats armor;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }
    public void TakeDamage(int damage)
    {
        //if the player has armor it should reduce the damage taken
        damage -= armor.GetValue();
        //To ensure that the damage doesn't get below zero
        damage = Mathf.Clamp(damage, 0, int.MaxValue); 

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            //Depends on who will die (Enemies Either death animation or physics particles) as for the player game over
            Die();
        }
    }

    //Can be either overrider for the enemy or the player
    public virtual void Die()
    {
        //Die in some way
        Debug.Log(transform.name + " died.");
    }
}
