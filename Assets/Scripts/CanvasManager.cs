using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Menus")]
    public GameObject SearchBar;
    public GameObject HighScoreEntryTemplate;
    public GameObject Background;

    [Header("Images")]
    public Image BackgroundImage;
    
    [Header("Text")]
    public Text searchText;
    public Text ScoreText;
    public Text LevelText;
    public Text PlayerText;
    public Text PosText;

    [Header("Buttons")]
    public Button GoButton;

    void Start()
    {
        if (GoButton)
            GoButton.onClick.AddListener(() => SearchEntry());
    }

    void Update()
    {

        // SearchEntry();
        // Don't know what to put here, but this is what I'm trying to code.
        
        // if searchText == entry of search
        // display entry
    }

    void SearchEntry()
    {
        
    }
}
