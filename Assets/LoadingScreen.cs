using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider loadingSlider;

    public void LoadGame(string name)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(AsynchronousLoad(name));
    }
    IEnumerator AsynchronousLoad(string scene)
    {
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        // ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
           
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            Debug.Log(ao.progress);
            loadingSlider.value = progress;
            yield return null;
        }
    }
}
