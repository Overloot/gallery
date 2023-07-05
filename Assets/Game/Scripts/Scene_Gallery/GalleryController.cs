using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using TogoEvents;

public class GalleryController : MonoBehaviour
{
    [SerializeField] private GalleryUnit gUnitPrefab;
    [SerializeField] private Transform galleryContent;
    [SerializeField] private ScrollViewTool scrollViewTool;

    [SerializeField] private const string link = "http://data.ikppbb.com/test-task-unity-data/pics/";
    [SerializeField] private const string fileExt = ".jpg";

    bool remoteFileExists;
    bool remoteFileChecked;

    private void Start()
    {
        StartCoroutine(nameof(CheckAvailableImagesAndCreateGalleryUnits));
    }

    /// <summary>
    /// проверка на существования файла по линку и создание под него префаба в галлерею
    /// 
    /// Если порядок нумерации не будет соблюдён, то создание префабов остановится и картинка
    /// с неправильной нумерацией('01' вместо '1') не будет отображаться в галерее
    /// 
    /// найдя несуществующий линк генерация префабов остановится
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckAvailableImagesAndCreateGalleryUnits()
    {
        LoadingManager.Instance.NextProgress();
        int imgNumber = 1;
        while (true)
        {
            yield return null;
            remoteFileChecked = false;
            remoteFileExists = CheckForRemoteFileExists(link + imgNumber.ToString() + fileExt);
            yield return new WaitWhile(() => remoteFileChecked != true);
            if (remoteFileExists)
            {
                GalleryUnit unit = Instantiate(gUnitPrefab, galleryContent);
                unit.Setup(imgNumber, this);
                imgNumber++;
            }
            else
            {
                break;
            }
        }
        LoadingManager.Instance.NextProgress();

        scrollViewTool.CheckVisibility();

        LoadingManager.Instance.CompleteProgress();
    }

    private bool CheckForRemoteFileExists(string url)
    {
        try
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "HEAD";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            response.Close();
            remoteFileChecked = true;
            return (response.StatusCode == HttpStatusCode.OK);
        }
        catch
        {
            remoteFileChecked = true;
            return false;
        }
    }

    public string GetLink()
    {
        return link;
    }

    public string GetFileExt()
    {
        return fileExt;
    }
}
