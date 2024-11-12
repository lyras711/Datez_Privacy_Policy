using UnityEngine;

[CreateAssetMenu(fileName = "QnA", menuName = "ScriptableObjects/QuestionAndAnswer", order = 1)]
public class QuestionsAndAnswers : ScriptableObject
{
    public string question;
    public int answer;
    public int marginForError = 10;
}
