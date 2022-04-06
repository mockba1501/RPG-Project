using UnityEngine;

public class Interactable : MonoBehaviour
{
    //Distance how close the player needs to be next to the interactable object (Item/Enemy)
    public float radius = 3f;

    //Draw graphics inside the scene
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
