using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITimePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Image sliderValue;

    private void Start()
    {
        Shared.gameMng.iSpawnTimer.UnTimerEvent(UpdateTimePanel);
        Shared.gameMng.iSpawnTimer.SubTimerEvent(UpdateTimePanel);
    }

    private void UpdateTimePanel()
    {
        float sec = Shared.gameMng.iSpawnTimer.GetSec();
        float maxSec = Shared.gameMng.iSpawnTimer.GetMaxSec();
        timerText.text = $"{(int)sec}s";
        sliderValue.fillAmount = sec / maxSec;
        //timerText.text = string.Format("{0:00}:{1:00}", min, (int)sec);
    }
}
