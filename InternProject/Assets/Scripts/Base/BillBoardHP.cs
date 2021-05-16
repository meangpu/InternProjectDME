using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardHP : MonoBehaviour
{

    [SerializeField] private Transform cam;

    private void Start() 
    {
        // performance issue but better than update
        cam = Camera.main.transform;
    }

    private void LateUpdate() 
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
