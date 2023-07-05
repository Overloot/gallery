using UnityEngine;
using UnityEngine.UI;
using TogoEvents;

public class BtnGallery : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(OnBtn);
    }

    void OnBtn()
    {
        EventManager.TriggerEvent(EventsCol.LoadScene, MsgColl.Gallery);
    }
}
