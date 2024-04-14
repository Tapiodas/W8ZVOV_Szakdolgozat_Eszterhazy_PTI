using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.VisualScripting;
using UnityEngine;

public class Gamearea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public enum WinState
    {
        PirosWin,
        SargaWin,
        
    }

    private static void TrackWinInTensorboard(WinState winState)
    {
        switch (winState)
        {
            case WinState.PirosWin:
                Academy.Instance.StatsRecorder.Add("Prios/FalnakUtkozott", 0, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/FalnakUtkozott", 1, StatAggregationMethod.Average);
                 break;
            case WinState.SargaWin:
                Academy.Instance.StatsRecorder.Add("Prios/FalnakUtkozott", 1, StatAggregationMethod.Average);
                Academy.Instance.StatsRecorder.Add("Sarga/FalnakUtkozott", 0, StatAggregationMethod.Average);
                break;
            
        }
    }


}
