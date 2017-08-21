using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZZBullet : MonoBehaviour {

    private float speed = 20f;
    public float xRate = 0;
    public float yRate = 0;
    public bool xPos;
    public bool yPos;
    private RectTransform myRt;
    private float sceneHeight;

    void Awake () {
        myRt = GetComponent<RectTransform>();
        sceneHeight = StaticMethods.sceneHeight;
    }

	void Update () {
        if (Mathf.Abs(myRt.anchoredPosition.x) > 480 / 2 + myRt.sizeDelta.x / 2 ||
            Mathf.Abs(myRt.anchoredPosition.y) > sceneHeight / 2 + myRt.sizeDelta.y / 2) {
            Destroy(gameObject);
            ZZPlayScene.bulletCount--;
            return;
        }

        if (xPos == true) {
            transform.Translate(-Mathf.Abs(xRate) * speed * Time.deltaTime, 0, 0);
        } else {
            transform.Translate(Mathf.Abs(xRate) * speed * Time.deltaTime, 0, 0);
        }
        if (yPos == true) {
            transform.Translate(0, -Mathf.Abs(yRate) * speed * Time.deltaTime, 0);
        } else {
            transform.Translate(0, Mathf.Abs(yRate) * speed * Time.deltaTime, 0);
        }
    }

}
