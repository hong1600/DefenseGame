using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenModeSetting : MonoBehaviour
{
    [SerializeField] TMP_Dropdown modeDropdown;
    [SerializeField] TextMeshProUGUI modeText;

    List<string> modeList = new List<string> { "â ���", "��ü ȭ��"};

    private void Start()
    {
        modeDropdown.options.Clear();
        modeDropdown.AddOptions(modeList);
        Screen.fullScreen = true;
        modeDropdown.value = Screen.fullScreen ? 1 : 0;
        SetFullScreen(modeDropdown.value);

        modeDropdown.onValueChanged.AddListener(SetFullScreen);
    }

    public void SetFullScreen(int _index)
    {
        if (_index == 0)
        {
            Screen.fullScreen = false;
            modeText.text = "â ���";
        }
        else if (_index == 1)
        {
            Screen.fullScreen = true;
            modeText.text = "��ü ȭ��";
        }

        Resolution res = Screen.currentResolution;
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
