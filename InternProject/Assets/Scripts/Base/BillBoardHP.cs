using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardHP : MonoBehaviour
{

    [SerializeField] private Transform cam;

    private void LateUpdate() 
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
