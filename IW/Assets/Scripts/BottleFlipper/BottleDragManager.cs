using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleDragManager : MonoBehaviour {

    Vector2 offset;
    public bool bottleIsDragging;
    private Vector2 initialPosition;

    void Awake () {
        initialPosition = GetComponent<RectTransform>().anchoredPosition;
    }

    public void MouseDown () {
        //translate the cubes position from the world to Screen Point
        //screenSpace = Camera.main.WorldToScreenPoint(transform.position);

        //calculate any difference between the cubes world position and the mouses Screen position converted to a world point  
        offset = transform.position - Input.mousePosition;
        bottleIsDragging = true;

    }

    public void MouseUp () {
        bottleIsDragging = false;
        GetComponent<RectTransform>().anchoredPosition = initialPosition;
    }

    void Update () {
        if (bottleIsDragging) {
            MouseDrag();
        }
    }

    void MouseDrag () {
        //keep track of the mouse position
        Vector2 curScreenSpace = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        //convert the screen mouse position to world point and adjust with offset
        Vector2 curPosition = new Vector2(curScreenSpace.x + offset.x, curScreenSpace.y + offset.y);

        //update the position of the object in the world
        transform.position = curPosition;
    }
}
