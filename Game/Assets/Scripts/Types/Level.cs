using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz/New Level")]
public class Level : ScriptableObject {
    public DificultName dificultName;
    public int totalLevelQestions;
    public Question[] allQuestions;
    public List<Question> remainingQuestions;
    public float timeToQuestion;

    
}
