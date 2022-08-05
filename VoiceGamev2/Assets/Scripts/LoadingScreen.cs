using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingScreen : MonoBehaviour
{
    [SerializeField] MoveDataToMain moveDataToMain;
    public GameObject loadingScreen;
    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }
    IEnumerator LoadSceneAsync(int sceneID)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        loadingScreen.transform.GetChild(0).gameObject.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            yield return null;
        }
    }
}
