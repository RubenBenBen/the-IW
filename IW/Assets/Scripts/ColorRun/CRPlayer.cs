using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CRPlayer : MonoBehaviour {

    public CRBlockContainer blockContainer;
    public GameObject swapObject;
    public GameObject myObject;
    private GameObject trueSwapObject;
    private Vector2 myPos;
    private Vector2 swapPos;
    private float timeRate = 0.01f;
    public CRPlaySceneHelper playSceneHelper;


    void Start () {
        Rect myRect = GetComponent<RectTransform>().rect;
        swapObject.GetComponent<RectTransform>().sizeDelta =
            myObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (myRect.height, myRect.width);
        Invoke("SetData", 1);
	}

    private void ChangeBlockColors () {
        blockContainer.ChangeBlockColors();
    }

    private void SetData () {
        SetColor();
        SetText();
        playSceneHelper.touchOff = false;
    }

    private void SetColor () {
        int rand = Random.Range(0, CRPlayScene.colors.Length);
        GetComponent<Image>().color = CRPlayScene.colors[rand];
    }

    private void SetText () {
        transform.GetChild(0).GetComponent<Text>().text = playSceneHelper.GetTextFromColor(GetComponent<Image>().color);
    }

    private void ChangeColors (GameObject obj) {
        Color saveColor = GetComponent<Image>().color;
        string saveText = transform.GetChild(0).GetComponent<Text>().text;
        GetComponent<Image>().color = obj.GetComponent<Image>().color;
        transform.GetChild(0).GetComponent<Text>().text = obj.transform.GetChild(0).GetComponent<Text>().text;
        obj.GetComponent<Image>().color = saveColor;
        obj.transform.GetChild(0).GetComponent<Text>().text = saveText;
    }

    public void SwapColors () {
        if (!playSceneHelper.touchOff) {
            trueSwapObject = EventSystem.current.currentSelectedGameObject;
            if (trueSwapObject.GetComponent<Image>().color != GetComponent<Image>().color) {
                playSceneHelper.touchOff = true;
                AdjustDublicates();
                InvokeRepeating("ObjectsAnimation", 0, timeRate);
                ChangeColors(trueSwapObject);
            }
        }
    }

    private void ObjectsAnimation () {
        if (Vector2.Distance(myObject.transform.position, swapPos) > 1) {
            myObject.transform.position = Vector2.Lerp(myObject.transform.position, swapPos, 0.1f);
            swapObject.transform.position = Vector2.Lerp(swapObject.transform.position, myPos, 0.1f);
        } else {
            CancelInvoke("ObjectsAnimation");
            myObject.transform.position = swapPos;
            swapObject.transform.position = myPos;
            trueSwapObject.SetActive(true);
            gameObject.SetActive(true);
            myObject.SetActive(false);
            swapObject.SetActive(false);
            Invoke("ChangeBlockColors", 0.25f);
        }
    }

    void AdjustDublicates () {
        swapObject.transform.position = trueSwapObject.transform.position;
        swapObject.GetComponent<Image>().color = trueSwapObject.GetComponent<Image>().color;
        swapObject.transform.Find("Text").GetComponent<Text>().text = trueSwapObject.transform.Find("Text").GetComponent<Text>().text;
        swapPos = trueSwapObject.transform.position;
        swapObject.SetActive(true);
        trueSwapObject.SetActive(false);

        myObject.transform.position = gameObject.transform.position;
        myObject.GetComponent<Image>().color = GetComponent<Image>().color;
        myObject.transform.Find("Text").GetComponent<Text>().text = transform.Find("Text").GetComponent<Text>().text;
        myPos = gameObject.transform.position;
        myObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
