using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LOPlayScene : MonoBehaviour {

    public SceneController sceneController;
    private Image darkPanel;
    private bool pressed;
    private float fadeOutRate;
    private float alpha = 1;
    private GameObject dontPressButton;
    private GameObject pressButton;
    public bool dontPressClicked;

    // Use this for initialization

    void OnApplicationPause () {
        if (dontPressClicked) {
            ChangeButtonText();
        }
    }

    void Awake () {
        darkPanel = transform.Find("DarkPanel").GetComponent<Image>();
        dontPressButton = transform.Find("DontPress").gameObject;
        pressButton = transform.Find("Press").gameObject;
        fadeOutRate = 0.005f;

    }

    public void DontPressClicked () {
        dontPressClicked = true;
    }

    public void GoToBackground () {
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call<bool>("moveTaskToBack", true);
    }

    public void ChangeButtonText () {
        dontPressButton.GetComponent<Button>().enabled = false;
        dontPressButton.transform.Find("Text").GetComponent<Text>().text = "Told Ya!";
        pressButton.transform.Find("Text").GetComponent<Text>().text = "LUL";
    }

    public void LightOffWin () {
        dontPressButton.GetComponent<Button>().enabled = false;
        pressButton.GetComponent<Button>().enabled = false;
        sceneController.GoToNextLevel();
    }

    public void HintPressed () {
        pressed = true;
    }

    public void HintPressEnded () {
        pressed = false;
        alpha = 1;
        darkPanel.color = Color.black;
    }

	void Update () {
        if (pressed) {
            alpha -= fadeOutRate;
            darkPanel.color = new Color(darkPanel.color.r, darkPanel.color.g, darkPanel.color.b, alpha);
            if (alpha <= 0) {
                darkPanel.gameObject.SetActive(false);
                pressButton.SetActive(true);
                dontPressButton.SetActive(true);
            }
        }
	}
}
