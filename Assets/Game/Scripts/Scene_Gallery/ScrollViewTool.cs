using UnityEngine;
using UnityEngine.UI;

public class ScrollViewTool : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private ScrollRect scrollRect;    

    private void Start()
    {
        // при листании списка будет вызываться метод проверки видимости объекта в камере
        scrollRect.onValueChanged.AddListener((blanck) =>
        {
            CheckVisibility();
        });        
    }

    /// <summary>
    /// проверка всех чилдов ScrollView Content на видимость в camera
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
