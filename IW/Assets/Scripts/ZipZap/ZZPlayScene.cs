using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ZZPlayScene : MonoBehaviour {

    Touch touchBegin;
    Touch touchEnd;
    public SceneController sceneController;
    Vector2 difference;
    public GameObject test;
    private bool xPos;
    private bool yPos;
    public static int bulletCount = 0;
    public Slider bulletSlider;
    private bool spawn;
    private int currentButtonValue = 0;

    private void Update () {
        if (spawn) {
            if (bulletCount <= 80) {
                bulletSlider.value = bulletCount;
                if (Input.touchCount == 1 && ( Input.GetTouch(0).phase == TouchPhase.Began )) {
                    touchBegin = Input.GetTouch(0);
                }
                if (Input.touchCount == 1 && ( Input.GetTouch(0).phase == TouchPhase.Moved )) {
                    touchEnd = Input.GetTouch(0);
                    difference = touchBegin.position - touchEnd.position;
                    if (touchBegin.position.x - touchEnd.position.x > 0) {
                        xPos = true;
                    } else {
                        xPos = false;
                    }
                    if (touchBegin.position.y - touchEnd.position.y > 0) {
                        yPos = true;
                    } else {
                        yPos = false;
                    }
                    CreateTest();
                    bulletCount++;
                    bulletSlider.value++;
                }
            } else {
                spawn = false;
                bulletSlider.value = 80;
                ZipZapWin();
            }
        }
    }

    public void buttonClicked (int buttonValue) {
        if (!spawn) {
            if (buttonValue == 0) {
                currentButtonValue = 1;
            } else if (buttonValue == currentButtonValue) {
                currentButtonValue++;
                if (currentButtonValue == 4) {
                    spawn = true;
                    bulletSlider.gameObject.SetActive(true);
                }
            } else {
                currentButtonValue = 0;
            }
        }
    }

    public void ZipZapWin () {
        sceneController.GoToNextLevel();
    }

    private void CreateTest () {
        GameObject bullet = Instantiate(test);
        ZZBullet bulletClass = bullet.GetComponent<ZZBullet>();
        bulletClass.xPos = xPos;
        bulletClass.yPos = yPos;
        bulletClass.xRate = difference.x;
        bulletClass.yRate = difference.y;
        bullet.transform.SetParent(transform);
        bullet.transform.position = touchBegin.position;
        RectTransform rectTransform = bullet.GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
        bullet.SetActive(true);
    }

}
