using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask; 
    //Reference to the scene camera
    Camera cam;
    PlayerMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        //Left mouse button
        if(Input.GetMouseButtonDown(0))
        {
            //Cast a ray from the camera towards whatever we clicked (Mouse position)
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; 

            //Add range of 100 & the movementMask
            if(Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //Testing purposes
                //Debug.Log("We hit " + hit.collider.name + " " +  hit.point);
                
                //Move the player towards what we hit, navMeshController will find the fastest path towards that point
                motor.MoveToPoint(hit.point);

                //Stop focusing any object

            }
        }

        //Right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                //Check if we hit an interactable
                //If we did set it as our focus

            }
        }
    }
}
