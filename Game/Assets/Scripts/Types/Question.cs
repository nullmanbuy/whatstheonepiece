using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz/New Question")]
public class Question : ScriptableObject
{
    public string questionAsk;
    public Option[] options = new Option[4];
}

[System.Serializable]
public class Option {
    public string optionText;
    public bool correctAnwser;
}

public enum DificultName{
    ReiDosPiratas = 0,
    Yonkou = 1,
    Estrela = 2,
    Supernova = 3,
    Pirata = 4
}

