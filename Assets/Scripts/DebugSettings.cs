using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSettings : MonoBehaviour
{
    [Header("Debug Settings Press B to Toggle ON/OFF")]
    [SerializeField] bool collisionOff = false;

    [Tooltip("while true Press L key to load next level")]
    [SerializeField] bool loadNextLevel = false;


    // Update is called once per frame
    void Update()
    {
        if (Debug.isDebugBuild) 
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
            collisionOff = !collisionOff;
            loadNextLevel = !loadNextLevel;
            }
        }
    }

    public bool GetCollisionOff()
    {
        return collisionOff;
    }

    public bool GetLoadNextLevel()
    {
        return loadNextLevel;
    }



}
