using UnityEngine;

public class Targetable : MonoBehaviour
{
    [SerializeField] private Transform aimAtPoint = null;

    public Transform GetAimAtPoint() => aimAtPoint;
}
