using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditorInternal;
using System;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;

    [SerializeField] RawImage transitionImage;
    [SerializeField] RawImage CharacterImage;

    [SerializeField] float duration = 2.0f;

    public static event Action OnTransitionEnded;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    public void StartTranstion()
    {
        transitionImage.gameObject.SetActive(true);
        StartCoroutine(FadeTransition());
    }

    private IEnumerator FadeTransition()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = elapsed / duration;
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(1f);

        yield return new WaitForSeconds(0.5f);
        CharacterImage.gameObject.SetActive(true);
        
        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = 1f - (elapsed / duration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(0f);
        transitionImage.gameObject.SetActive(false);

        OnTransitionEnded?.Invoke();
    }

    private void SetAlpha(float alpha)
    {
        Color c = transitionImage.color;
        c.a = alpha;
        transitionImage.color = c;
    }
}
