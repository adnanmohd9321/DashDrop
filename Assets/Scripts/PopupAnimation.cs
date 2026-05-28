using UnityEngine;
using TMPro;
using System.Collections;

public class PopupAnimation : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private Vector3 startScale;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        startScale = transform.localScale;
    }

    void OnEnable()
    {
        StopAllCoroutines();

        StartCoroutine(AnimatePopup());
    }

    IEnumerator AnimatePopup()
    {
        float time = 0;

        canvasGroup.alpha = 0;

        transform.localScale = Vector3.zero;

        Vector3 startPos = transform.localPosition;

        while (time < 1f)
        {
            time += Time.deltaTime * 3f;

            // Fade
            canvasGroup.alpha =
                Mathf.Lerp(0, 1, time);

            // Scale pop
            transform.localScale =
                Vector3.Lerp(
                    Vector3.zero,
                    startScale,
                    time
                );

            // Float upward
            transform.localPosition =
                startPos +
                Vector3.up * time * 40f;

            yield return null;
        }

        yield return new WaitForSeconds(1f);

        time = 0;

        while (time < 1f)
        {
            time += Time.deltaTime * 2f;

            canvasGroup.alpha =
                Mathf.Lerp(1, 0, time);

            yield return null;
        }
    }
}