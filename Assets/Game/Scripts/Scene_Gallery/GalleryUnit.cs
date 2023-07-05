using UnityEngine;
using UnityEngine.UI;
using TogoEvents;
using UnityEngine.Networking;
using System.Collections;

public class GalleryUnit : MonoBehaviour
{
    [SerializeField] private int imgNum; // можно byte, но представим что имеджей больше 255
    private bool loadImageAlreadyCalled;
    private bool imageLoaded;
    private GalleryController galleryController;
    private RawImage rawImage;
    private bool pressed;

    private Button button;

    private const string link = "http://data.ikppbb.com/test-task-unity-data/pics/";
    private const string fileExt = ".jpg";

    void Start()
    {
        button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(OnBtn);
        rawImage = GetComponent<RawImage>();                      
    }

    /// <summary>
    /// установка параметров при инстанцировании
    /// </summary>
    /// <param name="imgNum"></param>
    /// <param name="galleryController"></param>
    public void Setup(int imgNum, GalleryController galleryController)
    {
        this.imgNum = imgNum;
        this.galleryController = galleryController;
    }

    public void LoadImage()
    {
        if (loadImageAlreadyCalled)
            return;
        loadImageAlreadyCalled = true;
        StartCoroutine(nameof(DownloadImage));
    }

    IEnumerator DownloadImage()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(
            galleryController.GetLink() + imgNum.ToString() + galleryController.GetFileExt());
        yield return request.SendWebRequest();        
        if (request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log(request.error);
        else
        {
            rawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            imageLoaded = true;
        }
    }

    private void OnBtn()
    {
        if (!imageLoaded)
            return;
        ImageDispatcher.rawImage = rawImage;
        EventManager.TriggerEvent(EventsCol.LoadScene, MsgColl.FullScreenImage);
    }

}