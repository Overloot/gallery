using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TogoEvents;
using System.Collections.Generic;
using System.Security.Cryptography;

public class LoadAnim : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtProgress;    

    private RectTransform rectComponent;
    private Image imageComp;
    private bool up;

    private float rotateSpeed = 100f;
    private float openSpeed = .005f;
    private float closeSpeed = .01f;    

    private LoadingManager loadingManager;

    public void Setup(LoadingManager loadingManager)
    {
        this.loadingManager = loadingManager;
    }

    private void Start()
    {
        rectComponent = GetComponent<RectTransform>();
        imageComp = rectComponent.GetComponent<Image>();
        up = true;
    }

    private void FixedUpdate()
    {
        rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
        Anim();
        UpdateTextProgress();        
    }

    private void Anim()
    {
        float currentSize = imageComp.fillAmount;

        if (currentSize < .30f && up)
        {
            imageComp.fillAmount += openSpeed;
        }
        else if (currentSize >= .30f && up)
        {
            up = false;
        }
        else if (currentSize >= .02f && !up)
        {
            imageComp.fillAmount -= closeSpeed;
        }
        else if (currentSize < .02f && !up)
        {
            up = true;
        }
    }

    private void UpdateTextProgress()
    {        
        txtProgress.text = loadingManager.CurProgress.ToString("n0") + "%";
    }

    private void OnDisable()
    {
        txtProgress.text = "0%";
    }
}