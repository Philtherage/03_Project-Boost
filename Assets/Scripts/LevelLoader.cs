using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [Tooltip("level Load Delay in Seconds")]
    [SerializeField] float levelLoadDelay = 2f;

    int currentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadDelay());
    }

    IEnumerator LoadDelay()
    {
        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            
            yield return new WaitForSeconds(levelLoadDelay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        yield return new WaitForSeconds(levelLoadDelay);
       
        SceneManager.LoadScene(currentSceneIndex += 1);
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Level 1");
    }
}
