using TogoEvents;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnBack : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(OnBtn);
    }

    void OnBtn()
    {
        EventManager.TriggerEvent(EventsCol.UnloadScene, MsgColl.FullScreenImage);
    }
}
