using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;


    private void Awake()
    {
        entryContainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");

        // hides template at Awake
        entryTemplate.gameObject.SetActive(false);

        // Adding one extra entry with SCORE, NAME, and LEVEL
        // AddHighscoreEntry(10000, "BETHL", 4); <<--- since this has been entered and saved, it doesn't need to be added again.

        // To enter a pre-made high score
        /*highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry{ score = 452884, level = 4, name = "CLARK" },
            new HighscoreEntry{ score = 889433, level = 8, name = "STINE" },
            new HighscoreEntry{ score = 547781, level = 5, name = "ANNIE" },
            new HighscoreEntry{ score = 61458, level = 6, name = "BELLE" },
            new HighscoreEntry{ score = 604331, level = 6, name = "JOOON" },
            new HighscoreEntry{ score = 742594, level = 7, name = "AARON" },
        };
        */

        // loading what was saved in the JSON script
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);


        // Sort entry list by Score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++){
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++){
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score){
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }        

        // cycle through list
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

        // Save data on the JSON script (test code) | don't need it anymore but will keep for reference
        /* Highscores highscores = new Highscores { highscoreEntryList = highscoreEntryList };
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json); // saving key "highscoreTable" with value "100"
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
        */    
    }

    // Duplicating Entries
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;
        // this whole section duplicates entry amounts
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        // to edit text fields
        int rank = transformList.Count + 1; // positions level scores
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;;

        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.name;

        entryTransform.Find("PlayerText").GetComponent<Text>().text = name;

        int level = highscoreEntry.level;

        entryTransform.Find("LevelText").GetComponent<Text>().text = level.ToString();

        // Set background visible odds and evens, easier to read
        entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);
        if (rank == 1)
        {
            // Highlights first entry
            entryTransform.Find("PosText").GetComponent<Text>().color = Color.yellow;
            entryTransform.Find("ScoreText").GetComponent<Text>().color = Color.yellow;
            entryTransform.Find("PlayerText").GetComponent<Text>().color = Color.yellow;
            entryTransform.Find("LevelText").GetComponent<Text>().color = Color.yellow;
        }

        transformList.Add(entryTransform);
    }

    // Adding new highscore entry to saved list
    private void AddHighscoreEntry(int score, string name, int level)
    {
        // Create Highscore Entry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name, level = level };

        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private void SearchEntry()
    {
        
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /* 
     * Represents a single High score entry
     */
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
        public int level;
    }
}
