using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    public float progess;
    public void NextScene(string sceneName)
    {
        StartCoroutine(LoadSceneGame(sceneName));
    }

    IEnumerator LoadSceneGame(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            progess = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            if (progess == 1)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
