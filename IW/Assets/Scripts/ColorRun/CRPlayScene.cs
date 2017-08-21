using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CRPlayScene : MonoBehaviour {

    public static Color[] colors = new Color[6];

    void Awake () {
        SetupColors();
        SetChildrenHeights();
	}

    void SetChildrenHeights () {
        float height = StaticMethods.sceneHeight - 48 - 100; ////WTEGFWTFS
        Transform blockTr = transform.Find("BlockContainer");
        RectTransform blockRt = blockTr.GetComponent<RectTransform>();
        float blockChildCount = blockTr.childCount;
        float childPosY = 0;
        for (int i = 0 ; i < blockChildCount ; i++) {
            RectTransform childRt = blockTr.GetChild(i).GetComponent<RectTransform>();
            childRt.sizeDelta = new Vector2(
                childRt.sizeDelta.x, height / blockChildCount);
            childRt.anchoredPosition = new Vector2(childRt.anchoredPosition.x, childPosY);
            childPosY -= childRt.sizeDelta.y;
        }
    }

    private void SetupColors () {
        colors[0] = Color.cyan;
        colors[1] = Color.red;
        colors[2] = Color.green;
        colors[3] = Color.blue;
        colors[4] = Color.yellow;
        colors[5] = Color.magenta;
    }
}
