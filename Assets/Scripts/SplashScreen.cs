using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.5f;
    public float displayTime = 2f;
    public string nextSceneName = "MainScene";

    void Start()
    {
        StartCoroutine(PlaySplash());
    }

    IEnumerator PlaySplash()
    {
        // Fade In
        yield return StartCoroutine(Fade(0, 1));

        // Wait
        yield return new WaitForSeconds(displayTime);

        // Fade Out
        yield return StartCoroutine(Fade(1, 0));

        // Load Main Menu
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator Fade(float start, float end)
    {
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;
            canvasGroup.alpha = Mathf.Lerp(start, end, t);
            yield return null;
        }

        canvasGroup.alpha = end;
    }
}
