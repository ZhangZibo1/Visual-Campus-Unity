using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class nextScene : MonoBehaviour
{
    public int nextSceneNum;

    static int[] fourPreference = new int[4];
    // Start is called before the first frame update
    void Enable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.N)) 
        {
            changeToNextScene();
        }
    }
    void changeToNextScene() 
    {
        SceneManager.LoadScene(nextSceneNum);
    }
}
