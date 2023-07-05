using System.Collections;
using System.Collections.Generic;
using TogoEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMngr : MonoBehaviour
{   
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnEnable()
    {
        EventManager.StartListening(EventsCol.LoadScene, LoadScene);
        EventManager.StartListening(EventsCol.UnloadScene, UnloadScene);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventsCol.LoadScene, LoadScene);
        EventManager.StopListening(EventsCol.UnloadScene, UnloadScene);
    }

    void LoadScene(Dictionary<MsgType, object> message)
    {
        if (message.ContainsKey(MsgColl.Gallery))
        {
            LoadingManager.Instance.ShowLoadingScreen();
            StartCoroutine(WaitForLoadingScreenReadyAndLoadScene("gallery"));            
        }
        else if (message.ContainsKey(MsgColl.FullScreenImage))
        {
            LoadingManager.Instance.ShowLoadingScreen();
            StartCoroutine(WaitForLoadingScreenReadyAndLoadScene("fullscreen"));            
        }
    }

    void UnloadScene(Dictionary<MsgType, object> message)
    {        
        if (message.ContainsKey(MsgColl.FullScreenImage))
        {
            SceneManager.UnloadSceneAsync("fullscreen");
        }
    }

    IEnumerator WaitForLoadingScreenReadyAndLoadScene(string scene)
    {        
        yield return new WaitWhile(() => LoadingManager.Instance.IsFaded() != true);        
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene == SceneManager.GetSceneByName("gallery"))
        {            
            StartCoroutine(nameof(WaitForGallerySceneReady));            
        }
        if (scene == SceneManager.GetSceneByName("fullscreen"))
        {            
            StartCoroutine(nameof(WaitForFullScreenSceneReady));
        }        
    }    

    IEnumerator WaitForGallerySceneReady()
    {        
        yield return new WaitWhile(() => LoadingManager.Instance.Done != true);        
        LoadingManager.Instance.HideLoadingScreen();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("gallery"));
        SceneManager.UnloadSceneAsync("main");        
    }

    IEnumerator WaitForFullScreenSceneReady()
    {        
        yield return new WaitWhile(() => LoadingManager.Instance.Done != true);
        LoadingManager.Instance.HideLoadingScreen();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("fullscreen"));        
    }
}
