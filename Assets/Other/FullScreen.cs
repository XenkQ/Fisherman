using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreen : MonoBehaviour
{
    private void Awake()
    {
        Screen.fullScreen = true;
    }
}
