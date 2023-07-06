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

    private AsyncOperation asyncOperation;

    private void Start()
    {
        StartCoroutine(LoadAsync());
        StartCoroutine(StartLoading());    
    }

	IEnumerator LoadAsync()
    {
		//AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

		//while (!asyncLoad.isDone)
		//{
		//    yield return null;
		//}


		asyncOperation = SceneManager.LoadSceneAsync(1);
		asyncOperation.allowSceneActivation = false;

		// While the asynchronous operation to load the new scene is not yet complete, continue waiting
		while (!asyncOperation.isDone)
		{
			// Check if the load has completed
			if (asyncOperation.progress >= 0.9f)
			{
				// You can implement here a condition when to activate your scene
				// For example: if a loading screen animation has completed
				//asyncOperation.allowSceneActivation = true;

				// Optionally output the progress to the console
				Debug.Log("Scene Loaded");
			}

			yield return null;
		}


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
            //Debug.Log("End while");
        }

        Sequence sec = DOTween.Sequence();
        Debug.Log("1");
        sec.Append(ui_FadeBG.img_FadeBG.DOFade(1,0.3f)).OnComplete(ChangeScene);


        //Debug.Log("Change scene");
    }

    public void ChangeScene()
    {
        //SceneManager.LoadScene(1);
        StartCoroutine(LoadSceneOverTime());      
    }

    private IEnumerator LoadSceneOverTime()
	{
        if(asyncOperation.progress >= 0.9f)
		{
            SceneManager.LoadScene(1);
		}
		else
		{
            yield return new WaitForSeconds(1f);
            ChangeScene();
		}
	}
}
