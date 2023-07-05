using UnityEngine;
using UnityEngine.UI;

public class ScrollViewTool : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private ScrollRect scrollRect;    

    private void Start()
    {
        // ��� �������� ������ ��������� ����� ������� ������ � ��������� ������
        scrollRect.onValueChanged.AddListener((blanck) =>
        {
            CheckVisibility();
        });        
    }

    /// <summary>
    /// ��������� ��� ����� go Content ���� ������ ����� ������� ������ � ��������� ������
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
