using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryRoom : MonoBehaviour
{
    public TextMeshProUGUI WinOrLoseText;
    public TextMeshProUGUI FailsText;
    public TextMeshProUGUI TotalTimeText;
    public GameController Controller;
    private int Tries;
    private float Time;
    public GameObject carpetClassic;
    public GameObject carpetChrismas;
    public GameObject navidadParent;
    public GameObject ClassicParent;
    public GameObject WallFrame;
    public GameObject LeftWall;
    public GameObject RightWall;
    public Material classicMaterial; 
    public Material christmasMaterial;
    public GameObject topWall;

    // Start is called before the first frame update
    void Start()
    {
        Controller = FindObjectOfType<GameController>();
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        Tries = Controller.GetTotalTries();
        Time = Controller.GetTotalTime();
        if (Controller.ClassicMode)
        {
            Debug.Log("ClassicMode is true");
            ClassicMode();
        }
        else
        {
            Debug.Log("ClassicMode is false");
            ChrismasMode();
        }
        UpdateText();
    }
    public void UpdateText()
    {
        FailsText.text = "Fails: " + Tries;
        TotalTimeText.text = "Total time: " + Time;
    }

    public void ClassicMode()
    {
        navidadParent.SetActive(false);
        ClassicParent.SetActive(true);
        if (LeftWall != null && RightWall != null && WallFrame != null)
        {
            MeshRenderer leftWallRenderer = LeftWall.GetComponent<MeshRenderer>();
            MeshRenderer rightWallRenderer = RightWall.GetComponent<MeshRenderer>();
            MeshRenderer wallFrameRenderer = WallFrame.GetComponent<MeshRenderer>();
            MeshRenderer topWallRenderer = topWall.GetComponent<MeshRenderer>();
            if (leftWallRenderer != null && rightWallRenderer != null && wallFrameRenderer != null)
            {
                leftWallRenderer.material = classicMaterial;
                rightWallRenderer.material = classicMaterial;
                topWallRenderer.material = classicMaterial;
                Material[] materials = wallFrameRenderer.materials;
                materials[1] = classicMaterial; 
                wallFrameRenderer.materials = materials;

            }
        }
    }
    public void ChrismasMode()
    {
        navidadParent.SetActive(true);
        ClassicParent.SetActive(false);
        if (LeftWall != null && RightWall != null && WallFrame != null)
        {
            MeshRenderer leftWallRenderer = LeftWall.GetComponent<MeshRenderer>();
            MeshRenderer rightWallRenderer = RightWall.GetComponent<MeshRenderer>();
            MeshRenderer wallFrameRenderer = WallFrame.GetComponent<MeshRenderer>();
            MeshRenderer topWallRenderer = topWall.GetComponent<MeshRenderer>();
            if (leftWallRenderer != null && rightWallRenderer != null && wallFrameRenderer != null)
            {
                leftWallRenderer.material = christmasMaterial;
                rightWallRenderer.material = christmasMaterial;
                topWallRenderer.material = christmasMaterial;
                Material[] materials = wallFrameRenderer.materials;
                materials[1] = christmasMaterial;
                wallFrameRenderer.materials = materials;

            }
        }
    }
}
