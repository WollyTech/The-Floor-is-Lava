using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Text _timerText;
    private Text _heightText;

    // Start is called before the first frame update
    void Start()
    {
        _timerText = GameObject.Find("Time").GetComponent<Text>();
        _heightText = GameObject.Find("Height").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTime(int time)
    {
        _timerText.text = "Timer: " + time;
    }

    public void UpdateHeight(int height)
    {
        _heightText.text = "Height: " + height + "m";
    }
}