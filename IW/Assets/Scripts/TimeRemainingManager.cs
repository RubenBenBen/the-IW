using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Xml;

public class TimeRemainingManager : MonoBehaviour {

    private string worldTimeGetURL = "http://www.nanonull.com/TimeService/TimeService.asmx/getUTCTime";
    private DateTime currentTime;
    private bool timerStarted;
    private Text timeText;

    void Awake () {
        timeText = GetComponent<Text>();
        timeText.text = "";
        StartCoroutine(GetTime(worldTimeGetURL));
    }

    private IEnumerator GetTime (string url) {
        WWW www = new WWW(url);
        yield return www;

        ParseXml(www.text);
        if (www.error != null) {
            //Debug.Log(www.error);

        } else if (www.bytes != null) {
            if (www.bytes.Length != 0) {

            } else {
                //Debug.Log("zero bytes");

            }
        } else {
            //Debug.Log("empty bytes");

        }
    }

    void Update () {
        if (timerStarted) {

        }
    }

    private void StartTimer () {
        Debug.Log(currentTime);
        string time = "Time Left: " + currentTime.TimeOfDay;
        timeText.text = time;
    }

    private void ParseXml (string xml) {
        char[] charArray = {'<', '>'};
        string[] stringArr = xml.Split(charArray);
        foreach (string str in stringArr) {
            if (DateTime.TryParse(str,out currentTime)) {
                StartTimer();
                return;
            }
        }
    }
}
