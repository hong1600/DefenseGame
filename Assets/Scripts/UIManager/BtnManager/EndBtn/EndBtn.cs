using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBtn : BtnManager
{
    public void lobby()
    {
        SceneManager.LoadScene(1);
    }
}
