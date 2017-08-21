using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LBPlayScene : MonoBehaviour {

    public SceneController sceneController;
    public Slider loadingBar;
    public Text loadingText;
    public Text loadingText2;
    string loadingString = "Loading . . .";
    int textIndex = 0;
    float loadTimeRate = 2f;
    int loadCallCount = 0;
    int loadMax = 40;
    private Text buttonText;
    private int number = 0;
    private Image colorButton;

    void Awake () {
        buttonText = transform.Find("NumberButton").Find("Text").GetComponent<Text>();
        colorButton = transform.Find("ColorButton").GetComponent<Image>();
    }

    public void LoadingBarChanged () {
        if (loadingBar.value >= 100) {
            loadingBar.interactable = false;
            loadingBar.value = 100;
            CancelInvoke("loadingTextAnimation");
            CancelInvoke("ChangeLoadingBar");
            loadingText.text = "";
            loadingText2.text = "";
            textIndex = 0;
            loadingString = "Welcome ! AE";
            loadingText2.gameObject.SetActive(true);
            InvokeRepeating("loadingTextAnimation", 0.5f, 0.2f);
            Invoke("LoadingBarWin", 5.5f);

        }
    }

    public void IncrementButton () {
        number++;
        buttonText.text = number + "";
    }

    public void ChangeColorButton () {
        colorButton.color = new Color(Random.value, Random.value, Random.value, 1.0f);
    }

    private void LoadingBarWin () {
        sceneController.GoToNextLevel();
    }

    private void ChangeLoadingBar () {
        loadCallCount++;
        if (loadCallCount % 3 == 0) {
            loadingText.GetComponent<RectTransform>().Rotate(0, 0, 180);
            loadingText2.GetComponent<RectTransform>().Rotate(0, 0, 180);
        }
        if (loadingBar.value < loadMax) {
            loadingBar.value += Random.Range(5, 10);
        } else {
            loadingText2.gameObject.SetActive(true);
            loadingBar.value -= Random.Range(3, 8);
            loadMax = Mathf.Min(loadMax + 10, 90);
        }
        loadTimeRate += Random.Range(1f, 3f);
        Invoke("ChangeLoadingBar", loadTimeRate);
    }

    void Start () {
        InvokeRepeating("loadingTextAnimation", 0.5f, 0.2f);
        Invoke("ChangeLoadingBar", loadTimeRate);
    }

    private void loadingTextAnimation () {
        if (textIndex < loadingString.Length) {
            loadingText.text += loadingString[textIndex];
            loadingText2.text += loadingString[textIndex];
            textIndex++;
        } else {
            loadingText.text = "";
            loadingText2.text = "";
            textIndex = 0;
        }
    }

}
