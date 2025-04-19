using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStart : MonoBehaviour
{
    public void OnClick()
    {
        LoadingBar.LoadScene("Level_1");
    }
}