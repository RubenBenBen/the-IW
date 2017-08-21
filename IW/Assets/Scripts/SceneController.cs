using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

    private RectTransform myRectTransform;
    private float timeRate = 0.01f;
    private Vector2 goToPosition;
    //private float xRate;
    //private float yRate;
    private GameObject prevLevel;
    private GameObject currLevel;

    private int currentLevelIndex;

    private string[] levelNames;

    private void CreateLevelList () {
        levelNames = new string[365];
        levelNames[0] = "LoadingBar";
        levelNames[1] = "ZipZap";
        levelNames[2] = "ColorRun";
        levelNames[3] = "LightOff";
    }

    void Awake () {
        Application.targetFrameRate = 60;
        float globalScale = Screen.width / 480f; ////WTFEEFWT
        StaticMethods.sceneHeight = Screen.height / globalScale;
        myRectTransform = GetComponent<RectTransform>();
        CreateLevelList();
        StartFirstLevel();
    }

    private void StartFirstLevel () {
        currentLevelIndex = 0;
        prevLevel = transform.Find(levelNames[0]).gameObject;
        Transform level = transform.Find(levelNames[0]);
        myRectTransform.localPosition = -level.GetComponent<RectTransform>().localPosition;
        level.gameObject.SetActive(true);
       // xRate = ( goToPosition.x - myRectTransform.localPosition.x ) * timeRate;
       // yRate = ( goToPosition.y - myRectTransform.localPosition.y ) * timeRate;
    }

    public void GoToNextLevel () {
        currentLevelIndex++;
        ScrollToLevel(levelNames[currentLevelIndex]);
    }

    private void ScrollToLevel (string levelName) {
        Transform level = transform.Find(levelName);
        level.gameObject.SetActive(true);
        currLevel = level.gameObject;
        goToPosition = -level.GetComponent<RectTransform>().localPosition;
        //xRate = ( goToPosition.x - myRectTransform.localPosition.x ) * timeRate;
        //yRate = ( goToPosition.y - myRectTransform.localPosition.y ) * timeRate;
        InvokeRepeating("MoveToLevelPosition", 0f, timeRate);
    }

    private void MoveToLevelPosition () {
        myRectTransform.localPosition = Vector2.Lerp(myRectTransform.localPosition, goToPosition, 0.05f);
        if (Vector2.Distance(myRectTransform.localPosition, goToPosition) < 1) {
            myRectTransform.localPosition = goToPosition;
            CancelInvoke("MoveToLevelPosition");
            MoveCompleted();
        }
    }

    private void MoveCompleted () {
        goToPosition = Vector2.zero;
        //xRate = yRate = 0;
        prevLevel.SetActive(false);
        Destroy(prevLevel);
        prevLevel = currLevel;
    }
}
