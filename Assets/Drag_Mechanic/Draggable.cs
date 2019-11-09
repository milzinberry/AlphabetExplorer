using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [Header("- Place on draggable object -")]

    [Header("Place draggable slot in here")]
    public GameObject linked_slot;

    public void Lock ()
    {
        Collider col = GetComponent<Collider>();
        col.enabled = false;
    }
}
