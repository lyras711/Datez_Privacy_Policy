using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestionDataLoader : MonoBehaviour
{
    // Question structure
    public class Question
    {
        public string Category;
        public string Fact;
        public int Year;

        public Question(string category, string fact, int year)
        {
            Category = category;
            Fact = fact;
            Year = year;
        }
    }

    // Dictionary to hold questions by category
    public Dictionary<string, List<Question>> QuestionsByCategory = new Dictionary<string, List<Question>>();

    void Start()
    {
        LoadQuestionsFromCSV("final_facts_file");
    }

    void LoadQuestionsFromCSV(string fileName)
    {
        // Load CSV file as a text asset
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);

        if (csvFile == null)
        {
            Debug.LogError("CSV file not found in Resources folder.");
            return;
        }

        // Parse CSV content
        StringReader reader = new StringReader(csvFile.text);
        bool isHeader = true;

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();

            if (isHeader) // Skip header line
            {
                isHeader = false;
                continue;
            }

            string[] fields = line.Split(',');

            // Ensure correct field count
            if (fields.Length < 3)
            {
                Debug.LogWarning("Invalid line: " + line);
                continue;
            }

            // Parse fields
            string category = fields[0].Trim();
            string fact = fields[1].Trim();
            int year = int.Parse(fields[2].Trim());

            // Create and add the question to the dictionary
            Question question = new Question(category, fact, year);

            if (!QuestionsByCategory.ContainsKey(category))
            {
                QuestionsByCategory[category] = new List<Question>();
            }

            QuestionsByCategory[category].Add(question);
        }

        Debug.Log("Questions loaded successfully.");
    }

    public List<Question> GetQuestionsByCategory(string category)
    {
        if (QuestionsByCategory.ContainsKey(category))
        {
            return QuestionsByCategory[category];
        }
        else
        {
            Debug.LogWarning("Category not found: " + category);
            return new List<Question>();
        }
    }
}
