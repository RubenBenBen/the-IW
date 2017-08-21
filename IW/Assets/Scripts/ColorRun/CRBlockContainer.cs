using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CRBlockContainer : MonoBehaviour {

    private float colCount = 6; //max 8
    private float rowCount = 6; //max 6
    public SceneController sceneController;

    void Awake () {

    }

	void Start () {
        Invoke("ColorBlocks", 1);
    }

    public void ColorRunWin () {
        sceneController.GoToNextLevel();
    }

    public void ChangeBlockColors () {
        Color saveColor = transform.GetChild(0).GetChild(0).GetComponent<Image>().color;
        for (int j = 0 ; j < colCount ; j++) {
            Transform row = transform.GetChild(j);
            for (int q = 0 ; q < rowCount ; q++) {
                if (row.GetChild(q).GetComponent<Image>().color != saveColor) {
                    ChangeBlocks();
                    return;
                }
            }
        }
        Invoke("ColorRunWin", 0.5f);
    }

    private void ChangeBlocks () {
        for (int i = 0 ; i < colCount ; i++) {
            Transform row = transform.GetChild(i);
            for (int k = 0 ; k < rowCount ; k++) {
                Image img = row.GetChild(k).GetComponent<Image>();
                Text textClass = row.GetChild(k).GetChild(0).GetComponent<Text>();
                if (img.color == Color.cyan) {
                    img.color = CRPlayScene.colors[1];
                } else if (img.color == Color.red) {
                    img.color = CRPlayScene.colors[2];
                } else if (img.color == Color.green) {
                    img.color = CRPlayScene.colors[3];
                } else if (img.color == Color.blue) {
                    img.color = CRPlayScene.colors[4];
                } else if (img.color == Color.yellow) {
                    img.color = CRPlayScene.colors[5];
                } else if (img.color == Color.magenta) {
                    img.color = CRPlayScene.colors[0];
                }
                textClass.text = StaticMethods.GetTextFromColor(img.color);
            }
        }
        StaticMethods.touchOff = false;
    }

    public void ColorBlocks () {
        for (int i = 0 ; i < colCount ; i++) {
            Transform row = transform.GetChild(i);
            for (int k = 0 ; k < rowCount ; k++) {
                int rand = Random.Range(0, CRPlayScene.colors.Length);
                Image img = row.GetChild(k).GetComponent<Image>();
                img.color = CRPlayScene.colors[rand];
                row.GetChild(k).GetChild(0).GetComponent<Text>().text = StaticMethods.GetTextFromColor(img.color);
            }
        }
    }
}
