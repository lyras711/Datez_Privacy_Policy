using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CustomPackManager;

public class CreateYourOwn : MonoBehaviour
{
    CustomPackManager customPackManager;

    CustomQuestionPack customQuestionPack;
    public GameObject packNamePopup;
    
    public GameObject UI;
    public GameObject Menu;
    public GameObject questionInSet;
    public Transform setParent;

    public TMP_InputField questionInput;
    public TMP_Text addButton;
    public TMP_InputField packNameInput;
    public TMP_Text int1Text;
    public TMP_Text int2Text;
    public TMP_Text int3Text;
    public TMP_Text int4Text;
    private int int1 = 1;
    private int int2 = 9;
    private int int3 = 9;
    private int int4 = 9;
    private int submitedAnswer;

    private List<GameObject> createdQObj = new List<GameObject>();
    public List<string> questions = new List<string>();
    public List<int> answers = new List<int>();

    bool editing = false;
    int editQ;
    bool uiActive = false;

    bool editingCustomPack = false;

    private void Start()
    {
        customPackManager = GetComponent<CustomPackManager>();
    }

    public void TriggerUI()
    {
        customQuestionPack = new CustomQuestionPack("New Pack");
        editingCustomPack = false;

        uiActive = !uiActive;

        UI.SetActive(uiActive);
        Menu.SetActive(!uiActive);
    }

    public void AddToSet()
    {
        string questionToAdd = questionInput.text;
        submitedAnswer = (int1 * 1000) + (int2 * 100) + (int3 * 10) + (int4);

        if (editing)
        {
            addButton.text = "Add";
            createdQObj[editQ].transform.GetChild(0).GetComponent<TMP_Text>().text = questionToAdd;
            questions[editQ] = questionToAdd;
            answers[editQ] = submitedAnswer;
            editing = false;
        }
        else
        {
            GameObject newQuestion = Instantiate(questionInSet, setParent);
            createdQObj.Add(newQuestion);
            newQuestion.SetActive(true);
            newQuestion.transform.GetChild(0).GetComponent<TMP_Text>().text = questionToAdd;

            questions.Add(questionToAdd);
            answers.Add(submitedAnswer);

            newQuestion.GetComponent<Button>().onClick.AddListener(delegate { EditSetQuestion(newQuestion.transform); });
        }

        questionInput.text = "";
    }

    public void SubmitSet()
    {
        if (questions.Count == 0)
            return;

        List<CustomQuestion> newQuestions = new List<CustomQuestion>();

        for (int i = 0; i < questions.Count; i++)
        {
            CustomQuestion newQuestion = new CustomQuestion(questions[i], answers[i]);
            newQuestions.Add(newQuestion);
        }

        string oldName = customQuestionPack.PackName;
        customQuestionPack.PackName = packNameInput.text;

        customQuestionPack.Questions = newQuestions;

        if(editingCustomPack)
        {
            customPackManager.OverwriteSavedPack(oldName, customQuestionPack);
        }
        else
            customPackManager.SaveCustomPack(customQuestionPack);

        packNamePopup.SetActive(false);
        TriggerUI();
    }

    public void SetPackName()
    {
        packNamePopup.SetActive(true);
        packNameInput.text = customQuestionPack.PackName;
    }

    public void EditSetQuestion(Transform question)
    {
        addButton.text = "Update / Cancel";
        int i = question.GetSiblingIndex() - 1;
        editing = true;
        editQ = i;

        questionInput.text = questions[i];

        string answer = answers[i].ToString();

        var digits = answer.ToCharArray();

        if(digits.Length == 3)
        {
            int1 = 0;
            int2 = Convert.ToInt32(digits[0].ToString());
            int3 = Convert.ToInt32(digits[1].ToString());
            int4 = Convert.ToInt32(digits[2].ToString());
        }
        else if (digits.Length == 2)
        {
            int1 = 0;
            int2 = 0;
            int3 = Convert.ToInt32(digits[0].ToString());
            int4 = Convert.ToInt32(digits[1].ToString());
        }    
        else if (digits.Length == 1)
        {
            int1 = 0;
            int2 = 0;
            int3 = 0;
            int4 = Convert.ToInt32(digits[0].ToString());
        }
        else if(digits.Length == 0)
        {
            int1 = 0;
            int2 = 0;
            int3 = 0;
            int4 = 0;
        }
        else
        {
            int1 = Convert.ToInt32(digits[0].ToString());
            int2 = Convert.ToInt32(digits[1].ToString());
            int3 = Convert.ToInt32(digits[2].ToString());
            int4 = Convert.ToInt32(digits[3].ToString());
        }


        int1Text.text = int1.ToString();
        int2Text.text = int2.ToString();
        int3Text.text = int3.ToString();
        int4Text.text = int4.ToString();
    }

    public void UpdateCounterNumber1(bool up)
    {
        if (up)
        {
            int1++;
            if (int1 > 9)
                int1 = 0;
        }
        else
        {
            int1--;
            if (int1 < 0)
                int1 = 9;
        }
        int1Text.text = int1.ToString();
    }

    public void UpdateCounterNumber2(bool up)
    {
        if (up)
        {
            int2++;
            if (int2 > 9)
                int2 = 0;
        }
        else
        {
            int2--;
            if (int2 < 0)
                int2 = 9;
        }
        int2Text.text = int2.ToString();
    }

    public void UpdateCounterNumber3(bool up)
    {
        if (up)
        {
            int3++;
            if (int3 > 9)
                int3 = 0;
        }
        else
        {
            int3--;
            if (int3 < 0)
                int3 = 9;
        }
        int3Text.text = int3.ToString();
    }

    public void UpdateCounterNumber4(bool up)
    {
        if (up)
        {
            int4++;
            if (int4 > 9)
                int4 = 0;
        }
        else
        {
            int4--;
            if (int4 < 0)
                int4 = 9;
        }
        int4Text.text = int4.ToString();
    }

    public void EditCustomPack(CustomQuestionPack customQuestionPack)
    {
        uiActive = true;
        UI.SetActive(true);
        editingCustomPack = true;
        this.customQuestionPack = customQuestionPack;
        questionInput.text = "";

        for (int i = 0; i < customQuestionPack.Questions.Count; i++)
        {
            GameObject newQuestion = Instantiate(questionInSet, setParent);
            createdQObj.Add(newQuestion);
            newQuestion.SetActive(true);
            newQuestion.transform.GetChild(0).GetComponent<TMP_Text>().text = customQuestionPack.Questions[i].Fact;

            questions.Add(customQuestionPack.Questions[i].Fact);
            answers.Add(customQuestionPack.Questions[i].Year);

            newQuestion.GetComponent<Button>().onClick.AddListener(delegate { EditSetQuestion(newQuestion.transform); });
        }
    }
}
