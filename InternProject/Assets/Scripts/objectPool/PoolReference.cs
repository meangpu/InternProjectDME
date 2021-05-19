using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolReference : MonoBehaviour
{
    private Pooler pooler;

    public Pooler Pool
    {
        get { return pooler; }
        set { pooler = value; }
    }
}
