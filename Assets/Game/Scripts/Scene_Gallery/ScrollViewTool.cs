using UnityEngine;
using UnityEngine.UI;

public class ScrollViewTool : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private ScrollRect scrollRect;    

    private void Start()
    {
        // при листании списка проверяем какие объекты попали в видимость камеры
        scrollRect.onValueChanged.AddListener((blanck) =>
        {
            CheckVisibility();
        });        
    }

    /// <summary>
    /// проверяем все чилды go Content чтоб узнать какие объекты попали в видимость камеры
    /// </summary>
    public void CheckVisibility()
    {
        foreach (RectTransform child in scrollRect.content)
        {
            if (child.IsVisibleFrom(camera))
            {
                child.GetComponent<GalleryUnit>().LoadImage();
            }
        }
    }

}
