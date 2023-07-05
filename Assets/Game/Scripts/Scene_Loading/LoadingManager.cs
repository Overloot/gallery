using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private LoadAnim loadAnim;
    [SerializeField] private Fader fader;
    [SerializeField] private GameObject loadingScreenCanvas;
    [SerializeField] private GameObject uiParent;

    public int MaxAllowedProgress = 0;
    public float CurProgress = 0;

    public bool Done { private set; get; }

    public static LoadingManager Instance = null;

    private bool isDisabled = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);

        loadAnim.Setup(this);
    }

    private void FixedUpdate()
    {
        if (isDisabled) return;

        if (CurProgress >= 100) { Done = true; }
        else { CurProgress += 20 * Time.deltaTime; }
    }

    public void ShowLoadingScreen()
    {        
        Done = false;
        MaxAllowedProgress = 0;
        CurProgress = 0;
        loadingScreenCanvas.SetActive(true);
        fader.Fade();
        StartCoroutine(nameof(WaitForFade));
    }

    public void HideLoadingScreen()
    {
        isDisabled = true;
        fader.UnFade();
        StartCoroutine(nameof(WaitForUnFade));
    }

    IEnumerator WaitForFade()
    {
        yield return new WaitWhile(() => fader.IsFade != true);
        uiParent.SetActive(true);
        isDisabled = false;
    }

    IEnumerator WaitForUnFade()
    {
        yield return new WaitWhile(() => fader.IsFade != false);
        uiParent.SetActive(false);
        loadingScreenCanvas.SetActive(false);
    }

    /// <summary>
    /// добавляет 20 к максимальному прогрессу загрузки
    /// </summary>
    public void NextProgress()
    {
        MaxAllowedProgress += 20;
        if (MaxAllowedProgress > 100) MaxAllowedProgress = 100;
    }

    public void SetProgress(int progress)
    {
        MaxAllowedProgress = progress;
    }

    /// <summary>
    /// устанавливает прогресс загрузки в 100
    /// </summary>
    public void CompleteProgress()
    {
        MaxAllowedProgress = 100;
    }

    public bool IsFaded()
    {
        return fader.IsFade;
    }
}
