using System;
using UnityEngine;

public class GameSingleton : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public static GameSingleton singleton;
    public SphereController sphere;
    #endregion
    #region PRIVATE_VARIABLES
    [SerializeField] private Vector3 startPos;
    [SerializeField] private float playTime;
    [SerializeField] private bool inGame;
    [SerializeField] private TMPro.TextMeshProUGUI timeText;
    [SerializeField] private MenuController menuController;
    #endregion
    #region UNITY_CALLBACKS
    // Start is called before the first frame update
    void Start()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Debug.LogWarning("There is more than 1 Game Controller Singleton on scene!");
        }
        //Deactivate play camera when game is loaded
        Camera.main.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Count time passed if in game
        if(inGame) playTime += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(playTime);
        //Display formatted result as text
        timeText.text = timeSpan.ToString("mm':'ss':'fff");
        if (Input.GetButtonUp("Cancel")) CancelGame();
    }
    #endregion
    #region PUBLIC_METHODS
    public void InitializeGame(float customAcceleration)
    {
        //Enable controller script
        sphere.enabled = true;
        //Change game state
        inGame = true;
        //Reset time counting
        playTime = 0f;
        //Set sphere on start and make it possible to move
        sphere.transform.position = startPos;
        sphere.UnfreezeSphere();
        //Change acceleration amount
        sphere.acceleration = customAcceleration;
    }
    public void FallEnd()   //Behaviour for falling off track
    {
        EndGame();
        //Return to menu
        menuController.BackToTitle();
        //Call results screen
        menuController.ShowResults();
        menuController.headerText.text = "You Lose";
        menuController.resultsText.text = "You lost. Time not recorded.";
    }
    public void FinishEnd()     //Behaviour for reaching finish line
    {
        EndGame();
        //Record time in game
        PlayerPrefs.SetFloat("ClearTime", playTime);
        //Return to menu
        menuController.BackToTitle();
        //Call results screen
        menuController.ShowResults();
        menuController.headerText.text = "You Win";
    }
    #endregion
    #region PRIVATE_METHODS
    private void EndGame()
    {
        //Disable controller script
        sphere.enabled = false;
        //Change game state
        inGame = false;
        //Set sphere impossible to move
        sphere.FreezeSphere();
        //Change Cameras
        menuController.playCamera.gameObject.SetActive(false);
        menuController.menuCamera.gameObject.SetActive(true);
        sphere.transform.position = startPos;
    }
    private void CancelGame()
    {
        EndGame();
        //Return to menu
        menuController.BackToTitle();
    }
    #endregion
}
