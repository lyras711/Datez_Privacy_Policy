using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CustomPackManager : MonoBehaviour
{

    [System.Serializable]
    public class CustomQuestion
    {
        public string Fact;
        public int Year;

        public CustomQuestion(string fact, int year)
        {
            Fact = fact;
            Year = year;
        }
    }

    [System.Serializable]
    public class CustomQuestionPack
    {
        public string PackName;
        public List<CustomQuestion> Questions;

        public CustomQuestionPack(string packName)
        {
            PackName = packName;
            Questions = new List<CustomQuestion>();
        }
    }



    public GameObject UI;
    public GameObject Menu;

    public GameObject savedSet;
    public Transform savedSetsParent;

    private string customPacksFolderPath;
    public List<CustomQuestionPack> CustomPacks = new List<CustomQuestionPack>();

    bool UIActive = false;

    private void Start()
    {
        customPacksFolderPath = Path.Combine(Application.persistentDataPath, "CustomPacks");
    }

    public void TriggerUI()
    {
        UIActive = !UIActive;

        UI.SetActive(UIActive);
        Menu.SetActive(!UIActive);

        if(UIActive)
            LoadSavedPacks();
    }

    void LoadSavedPacks()
    {
        if (!Directory.Exists(customPacksFolderPath))
        {
            Debug.LogWarning("Custom packs folder not found. Creating new folder at: " + customPacksFolderPath);
            Directory.CreateDirectory(customPacksFolderPath);
            return;
        }

        CustomPacks.Clear();
        if (savedSetsParent.childCount > 1)
        {
            foreach (Transform child in savedSetsParent)
            {
                if (child != savedSetsParent.GetChild(0))
                {
                    Destroy(child.gameObject);
                }
            }
        }

        // Get all files in the custom packs folder
        string[] files = Directory.GetFiles(customPacksFolderPath, "*.json");

        foreach (string filePath in files)
        {
            string jsonContent = File.ReadAllText(filePath);
            CustomQuestionPack pack = JsonUtility.FromJson<CustomQuestionPack>(jsonContent);
            CustomPacks.Add(pack);

            GameObject newPack = Instantiate(savedSet, savedSetsParent);
            newPack.SetActive(true);
            newPack.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = pack.PackName;
        }

        Debug.Log("Loaded " + CustomPacks.Count + " custom packs.");
    }

    // Load questions for a specific pack by name
    public List<CustomQuestion> GetQuestionsFromPack(string packName)
    {
        CustomQuestionPack pack = CustomPacks.Find(p => p.PackName == packName);
        return pack != null ? pack.Questions : new List<CustomQuestion>();
    }

    // Save a custom question pack to a JSON file
    public void SaveCustomPack(CustomQuestionPack pack)
    {
        string json = JsonUtility.ToJson(pack);
        string path = Path.Combine(Application.persistentDataPath + "/CustomPacks/", pack.PackName + ".json");
        File.WriteAllText(path, json);
        Debug.Log("Custom pack saved at: " + path);
    }

    // Load a custom question pack from a JSON file
    CustomQuestionPack LoadCustomPack(string packName)
    {
        string path = Path.Combine(Application.persistentDataPath + "/CustomPacks/", packName + ".json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<CustomQuestionPack>(json);
        }
        else
        {
            Debug.LogWarning("Pack not found: " + path);
            return null;
        }
    }

    public void OverwriteSavedPack(string oldName, CustomQuestionPack pack)
    {
        string json = JsonUtility.ToJson(pack);
        string path = Path.Combine(Application.persistentDataPath + "/CustomPacks/", pack.PackName + ".json");
        string oldPath = Path.Combine(Application.persistentDataPath + "/CustomPacks/", oldName + ".json");
        File.Delete(oldPath);
        File.WriteAllText(path, json);
        Debug.Log("Custom pack Overwritten at: " + path);
    }

    public void PlayCustomPack(TMPro.TMP_Text text)
    {
        string packName = text.text;

        CustomQuestionPack packToPlay = LoadCustomPack(packName);

        TriggerUI();
        GetComponent<GameManager>().SelectCustomPack(packToPlay);

        Debug.Log("Opening and loading pack: " + packName);
    }

    public void EditPack(TMPro.TMP_Text text)
    {
        string packName = text.text;

        CustomQuestionPack packToEdit = LoadCustomPack(packName);


        GetComponent<CreateYourOwn>().EditCustomPack(packToEdit);
        UIActive = false;
        UI.SetActive(UIActive);
    }
}
