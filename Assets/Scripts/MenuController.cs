using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MenuController : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    //States Cameras
    public Camera menuCamera;
    public Camera playCamera;
    //Results Canvas Dynamic Elements
    public TextMeshProUGUI resultsText;
    public TextMeshProUGUI headerText;
    #endregion
    #region PRIVATE_VARIABLES
    //Menu Canvases
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas resultsCanvas;
    [SerializeField] private Canvas playCanvas;

    //Custom Acceleration Field
    [SerializeField] private TMP_InputField accelInput;

    #endregion

    #region UNITY_CALLBACKS
    // Start is called before the first frame update
    void Start()
    {
        BackToTitle();
    }
    #endregion
    
    #region PUBLIC_METHODS
    public void StartGame()
    {
        if (float.TryParse(accelInput.text, out float value))
        {
            playCamera.gameObject.SetActive(true);
            menuCamera.gameObject.SetActive(false);
            playCanvas.gameObject.SetActive(true);
            menuCanvas.gameObject.SetActive(false);
            GameSingleton.singleton.InitializeGame(float.Parse(accelInput.text));
        }
        else
        {
            Debug.LogWarning("Custom Acceleration field is not a number");
        }
    }
    public void ShowResults()
    {
        //Change Active Canvas
        menuCanvas.gameObject.SetActive(false);
        resultsCanvas.gameObject.SetActive(true);
        //Get last results from PlayerPrefs and format it
        float resultTime = PlayerPrefs.GetFloat("ClearTime", 0f);
        TimeSpan timeSpan = TimeSpan.FromSeconds(resultTime);
        //Display formatted result as text
        resultsText.text = timeSpan.ToString("mm':'ss':'fff");
        headerText.text = "Last Recorded Results";
    }
    public void BackToTitle()
    {
        menuCanvas.gameObject.SetActive(true);
        resultsCanvas.gameObject.SetActive(false);
        playCanvas.gameObject.SetActive(false);
    }
    #endregion
    
    #region PRIVATE_METHODS
    #endregion
}
