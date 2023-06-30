using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private Slider slider_Loading;
    [SerializeField] private float flt_LoadingDuration;
    [SerializeField] private LoadingFadeBGUI ui_FadeBG;
    [SerializeField] private float currentLoadingDuration;

    private void Start()
    {

        //Invoke("Loadscene", 2f);
        StartCoroutine(StartLoading());
        //StartCoroutine(LoadAsync());
    }

    private void Loadscene()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        // While the scene isn't fully loaded
        while (!operation.isDone)
        {
            // You can access operation.progress for a value between 0 and 0.9
            // that represents the current load progress
            // Note: operation.progress will never reach 1.0 directly,
            // when the scene is loaded the operation will just finish
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            // Yield until next frame
            yield return null;
        }

        Debug.Log("Loading completed");
    }

    IEnumerator StartLoading()
    {
        float startingValue = 0f;
        float endValue = slider_Loading.maxValue;
        
        while(currentLoadingDuration <= 1)
        {
            currentLoadingDuration += Time.deltaTime/ flt_LoadingDuration;
            float progress = Mathf.Lerp(startingValue, endValue, currentLoadingDuration);
            slider_Loading.value = progress;
           
            yield return null;
        }

        Sequence sec = DOTween.Sequence();
        sec.Append(ui_FadeBG.img_FadeBG.DOFade(1,0.3f)).OnComplete(ChangeScean);


        Debug.Log("Change scene");
    }

    public void ChangeScean()
    {
        SceneManager.LoadScene(1);
    }
}
