using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrOrientation : MonoBehaviour
{
    private void OnEnable()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    private void OnDisable()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
