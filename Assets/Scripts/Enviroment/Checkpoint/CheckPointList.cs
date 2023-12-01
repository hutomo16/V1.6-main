using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointList : MonoBehaviour
{
    public List<Transform> checkpointList;
    public static CheckPointList instance;

    public void Awake()
    {
        instance = this;
    }
    
}
