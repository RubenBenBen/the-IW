using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRPlaySceneHelper : MonoBehaviour {

    public bool touchOff = true;

    public string GetTextFromColor (Color color) {
        string text = "";
        if (color == Color.cyan) {
            text = "1";
        } else if (color == Color.red) {
            text = "2";
        } else if (color == Color.green) {
            text = "3";
        } else if (color == Color.blue) {
            text = "4";
        } else if (color == Color.yellow) {
            text = "5";
        } else if (color == Color.magenta) {
            text = "6";
        }
        return text;
    }
}
