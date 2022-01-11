using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public Controller controller;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Plus")
        {
            Debug.Log("Plus collision");

            controller.PlusCollision(collision);
        }

        if (collision.tag == "Cube")
        {
            Debug.Log("Cube collision");

            controller.CubeCollision(collision);
        }
    }
    

}
