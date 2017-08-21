using UnityEngine;
using System.Collections;

public class StaticMethods : MonoBehaviour {

    public static bool touchOff = true;
    public static float sceneHeight;

    public static string GetTextFromColor (Color color) {
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
