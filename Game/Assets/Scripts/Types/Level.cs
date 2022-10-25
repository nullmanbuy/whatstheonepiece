using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz/New Level")]
public class Level : ScriptableObject {
    public DificultName dificultName;
    public Question[] questionsData;
    public List<Question> dificultQuestions;
    public float timeToQuestion;

    
}
