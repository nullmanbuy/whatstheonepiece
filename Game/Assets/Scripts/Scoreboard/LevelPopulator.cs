using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using UnityEngine.UI;

[ExecuteInEditMode]
public class LevelPopulator : MonoBehaviour
{
    [SerializeField] ScoreboardController _scoreboard;
    [SerializeField] Level[] levels;
    [Header("Scoreboard")]
    [SerializeField] GameObject levelPrefab;
    [SerializeField] GameObject levelColumn;
     public int levelChildreenCount {
        get => levelColumn.transform.childCount;
    }

    [SerializeField] GameObject questionBoxPrefab;


    [Button("Populate")]
    private void PopulateLevelHolder() {
        if (levelColumn.transform.childCount <= 0) {
            for (int i = 0; i < levels.Length; i++) {
            Level currentLevel = levels[i];
            GameObject levelHolder = Instantiate(levelPrefab, levelColumn.transform) as GameObject;
            levelHolder.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().SetText(currentLevel.dificultName.ToString());
            _scoreboard.levelIndicators.Add(levelHolder.transform.GetChild(0).GetChild(0).gameObject);    

                for (int j = 0; j < currentLevel.totalLevelQestions; j++) {
                    GameObject currentQuestionBox = Instantiate(questionBoxPrefab, levelHolder.transform.GetChild(1)) as GameObject;
                    _scoreboard.allQuestionBox.Add(currentQuestionBox);

                }
            }
            _scoreboard.allQuestionBox.Reverse();
            _scoreboard.levelIndicators.Reverse();
        }
        
    }

    [Button("Clean Levels")]
    private void CleanLevelColumn() {
        while (levelChildreenCount > 0) {
            Transform lastChildreen = levelColumn.transform.GetChild(levelChildreenCount - 1);
            DestroyImmediate(lastChildreen.gameObject);

            if(levelChildreenCount == 0)
            break;
        }
        _scoreboard.allQuestionBox = new List<GameObject>();
        _scoreboard.levelIndicators = new List<GameObject>();
    }
}
