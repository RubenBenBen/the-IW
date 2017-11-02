using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomCollisionManager : MonoBehaviour {

    public BottleFlipperManager bottleFlipperManager;

    void OnCollisionEnter2D (Collision2D coll) {
        if (coll.gameObject.tag == "bottomCollider") {
            bottleFlipperManager.ResetCap();
        } else if (coll.gameObject.name == "Bottle") {
            bottleFlipperManager.BottleTouched();
        }
    }
}
