using UnityEngine;

public class Interactable : MonoBehaviour
{
    //Distance how close the player needs to be next to the interactable object (Item/Enemy)
    public float radius = 3f;
    public Transform interactionTransform;

    //Need to know whether we are being focused
    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    //All interactable object children classes will use it and define it in their specific ways
    public virtual void Interact()
    {
        //This function is to be overwritten by subclassess
        Debug.Log("Interacting with " + transform.name);
    }
    private void Update()
    {
        if(isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(interactionTransform.position, player.position);
            if (distance <= radius)
            {
                //Debug.Log("INTERACT");
                Interact();
                hasInteracted = true;
            }

        }
    }


    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;  
        hasInteracted = false;
    }

    public void onDefocused()
    {
        isFocus = false;
        player = null;      
        hasInteracted = false;

    }

    //Draw graphics inside the scene
    private void OnDrawGizmosSelected()
    {
        if(interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }


}
