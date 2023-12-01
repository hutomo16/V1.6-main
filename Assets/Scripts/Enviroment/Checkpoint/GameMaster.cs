using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public int CheckpointIndex;

    public static GameMaster instance;
    public Transform LastCheckPointPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            if(SceneManager.GetActiveScene().name == ("Map-Construction"))
            Destroy(gameObject);
        }
    }

    public void setLastCheckPoint(Transform checkpoint)
    {
        LastCheckPointPos = checkpoint;
        CheckpointIndex = CheckPointList.instance.checkpointList.IndexOf(checkpoint);
    }

    public void getLastCheckPoint()
    {
        LastCheckPointPos = CheckPointList.instance.checkpointList[CheckpointIndex];
        
    }

}
