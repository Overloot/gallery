using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageViewer : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;    

    void Start()
    {
        LoadingManager.Instance.CompleteProgress();

        rawImage.texture = ImageDispatcher.rawImage.texture;
        int w = Screen.width;
        int h = Screen.height;

        int sizeX;
        int sizeY;

        if (w > h) { sizeX = h; } else { sizeX = w; }
        sizeY = sizeX;

        rawImage.rectTransform.sizeDelta = new Vector2(sizeX, sizeY);

        
    }
}
