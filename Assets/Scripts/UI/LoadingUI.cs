using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private Slider slider_Loading;
    [SerializeField] private float flt_LoadingDuration;

    [SerializeField] private float currentLoadingDuration;

    private void Start()
    {

        //Invoke("Loadscene", 2f);
        StartCoroutine(StartLoading());
    }

    private void Loadscene()
    {
        SceneManager.LoadScene(2);
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

        Debug.Log("Change scene");
        SceneManager.LoadScene(2);
    }
}
