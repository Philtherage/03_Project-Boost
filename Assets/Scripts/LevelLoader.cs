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

    private void Update()
    {
        DebugLoadNextLevel();
    }

    public IEnumerator LoadDelay(bool isDead)
    {
        if (isDead)
        {
            yield return new WaitForSeconds(levelLoadDelay);
            SceneManager.LoadScene(0);
        }

        if (currentSceneIndex == (SceneManager.sceneCountInBuildSettings - 1))
        {
            yield return new WaitForSeconds(levelLoadDelay);
            SceneManager.LoadScene(0);
        }

        yield return new WaitForSeconds(levelLoadDelay);
        SceneManager.LoadScene(currentSceneIndex += 1);
    }

    private void DebugLoadNextLevel()
    {
        if (!FindObjectOfType<DebugSettings>()) { return; }

        if (FindObjectOfType<DebugSettings>().GetLoadNextLevel())
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (currentSceneIndex == (SceneManager.sceneCountInBuildSettings - 1))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else
                {
                    SceneManager.LoadScene(currentSceneIndex += 1);
                }
            }
            
        }
    }

}
