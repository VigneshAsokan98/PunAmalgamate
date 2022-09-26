using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    public Transform Flag;

    public LayerMask layerMask;

    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward,out hit,300, layerMask,QueryTriggerInteraction.Collide))
        {
            Debug.LogError("Wall Hit!!!");
            Flag.gameObject.SetActive(true);
            Flag.position = hit.point; 
        }
    }

}
