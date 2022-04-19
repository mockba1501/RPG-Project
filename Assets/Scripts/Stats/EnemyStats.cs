using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    //Change how we will die
    public override void Die()
    {
        base.Die();

        //Add ragdoll effect / death animation

        Destroy(gameObject);
        
        //this will also be a good place to add loot
    }
}
