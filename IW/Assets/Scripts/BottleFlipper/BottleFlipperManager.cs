using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BottleFlipperManager : MonoBehaviour {

    public SceneController sceneController;

    private BottleDragManager bottleDragManager;

    private Rigidbody2D cap;
    private Vector2 capInitialPosition;
    private Vector2 finalPosition;
    private bool kickStarted;
    private int kickCount;
    private Text kickCountText;
    private bool bottleIsDraggable;
    private bool bottleTouched;
    private int winCount = 15;

    private bool capPlanted;

    private RectTransform capRectTransform;
    private RectTransform bottleRectTransform;

    void Awake () {
        cap = transform.Find("Cap").GetComponent<Rigidbody2D>();
        capRectTransform = cap.GetComponent<RectTransform>();
        capInitialPosition = capRectTransform.anchoredPosition;
        finalPosition = new Vector2(0, capRectTransform.sizeDelta.y / 2f);

        kickCountText = transform.Find("KickCountText").GetComponent<Text>();

        bottleDragManager = transform.Find("Bottle").GetComponent<BottleDragManager>();
        bottleRectTransform = bottleDragManager.GetComponent<RectTransform>();
    }

    public void KickCap () {
        if (!kickStarted && !bottleDragManager.bottleIsDragging) {
            kickStarted = true;
            float directionX = cap.transform.position.x > Input.mousePosition.x ? 1 : -1;
            float directionY = cap.transform.position.y > Input.mousePosition.y ? 1 : 1;
            float forceRate = 15000;
            cap.AddForceAtPosition(new Vector2(forceRate * directionX, 30000 * directionY), Input.mousePosition);
            cap.gravityScale = 20;
        }
    }

    void Update () {
        if (!capPlanted && (bottleDragManager.bottleIsDragging || Mathf.Abs(cap.angularVelocity) < 5)) {
            if (Mathf.Abs(capRectTransform.transform.position.x - bottleRectTransform.transform.position.x) < 10) {
                float capY = cap.transform.position.y - cap.GetComponent<RectTransform>().sizeDelta.y / 2f;
                float bottleY = bottleDragManager.transform.position.y + bottleRectTransform.sizeDelta.y;
                //Debug.Log(capY);
                //Debug.Log(bottleY);
                if (capY - bottleY < 5) {
                    Invoke("CapPlanted", 2);
                }
            }
        }
    }

    private void CapPlanted () {
        Debug.Log("ASDFG");
        if (!capPlanted) {
            capPlanted = true;
            cap.transform.parent = bottleDragManager.transform;
            Destroy(cap.GetComponent<BoxCollider2D>());
            Destroy(cap);
            kickStarted = false;
            bottleIsDraggable = true;
            Destroy(bottleDragManager.GetComponent<EventTrigger>());
            bottleDragManager.MouseUp();
            Vector2 pos = bottleDragManager.transform.position;
            pos.x = Screen.width / 2f;
            bottleDragManager.transform.position = pos;
        }

    }

    public void BottleTouched () {
        if (!bottleTouched) {
            bottleTouched = true;
            kickCount++;
            if (kickCount >= winCount - 10) {
                if (kickCount >= winCount) {
                    kickCountText.color = Color.green;
                    kickCountText.text = "0";
                    bottleIsDraggable = true;
                    kickCount = -1000000;
                } else {
                    kickCountText.text = winCount - kickCount + "";
                }
            }
        }

    }

    public void DragBottle () {
        if (!capPlanted) {
            if (!kickStarted && bottleIsDraggable) {
                bottleDragManager.MouseDown();
                cap.isKinematic = true;
            } else {
                bottleDragManager.MouseUp();
                cap.isKinematic = false;
            }
        }

    }

    public void ResetCap () {
        cap.velocity = Vector2.zero;
        cap.angularVelocity = 0;
        cap.transform.rotation = Quaternion.Euler(Vector2.zero);
        cap.gravityScale = 0;
        cap.GetComponent<RectTransform>().anchoredPosition = capInitialPosition;
        kickStarted = false;
        bottleTouched = false;
    }
}
