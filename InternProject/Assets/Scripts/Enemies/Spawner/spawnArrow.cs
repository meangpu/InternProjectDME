using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnArrow : MonoBehaviour
{
    [SerializeField] GameObject arrowImage;
    [SerializeField] GameObject playerTarget;
    Renderer rd;

    private void Start() 
    {
        rd = GetComponent<Renderer>();
    }

    private void Update() {
        if (!rd.isVisible)
        {
            if(!arrowImage.activeSelf)  // if it still not show as arrow
            {
                arrowImage.SetActive(true);
            }

            Vector2 dir = playerTarget.transform.position - transform.position;

            RaycastHit2D ray = Physics2D.Raycast(transform.position, dir);

            if (ray.collider != null)
            {
                // Debug.Log(ray.collider);
                arrowImage.transform.position = ray.point;
                if (ray.collider.CompareTag("CamConfine"))
                {
                    Debug.Log("AAAAAAAAAAAAAAAAA");
                    arrowImage.transform.position = ray.point;
                }
            }

            else
            {
                if(arrowImage.activeSelf)
                {
                    arrowImage.SetActive(false);
                }
            }

        }
    }


}
