using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArrowBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void WindUp()
    {
        StartCoroutine(SliderCoroutine());
    }

    private IEnumerator SliderCoroutine()
    {
        float elapsedTime = 0f;
        float duration = 1f; // Duration of 1 second
        slider.value = 0; // Start at 0%

        while (elapsedTime < duration)
        {
            slider.value = Mathf.Lerp(0, 1, elapsedTime / duration); // Lerp from 0% to 100%
            fill.color = gradient.Evaluate(slider.value);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        slider.value = 1; // Ensure it's set to 100% at the end
    }
}
