using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    private Image fader;

    public bool IsFade { private set; get; }

    private void Awake()
    {
        fader  = GetComponent<Image>();
    }

    public void Fade()
    {
        StartCoroutine(Fading());
    }

    public void UnFade()
    {
        StartCoroutine(Unfading());
    }

    IEnumerator Fading()
    {
        float a = fader.color.a;
        for (float i = a; i < 1; i += 3 * Time.deltaTime)
        {
            fader.color = new Color(0, 0, 0, i);
            yield return new WaitForFixedUpdate();
        }
        fader.color = new Color(0, 0, 0, 1);
        IsFade = true;
    }

    IEnumerator Unfading()
    {
        float a = fader.color.a;
        for (float i = a; i > 0; i -= 3 * Time.deltaTime)
        {
            fader.color = new Color(0, 0, 0, i);
            yield return new WaitForFixedUpdate();
        }
        fader.color = new Color(0, 0, 0, 0);
        IsFade = false;
    }


}

