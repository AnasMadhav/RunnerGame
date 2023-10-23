using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class JSONFetcher : MonoBehaviour
{
    private string jsonUrl = "https://drive.google.com/uc?export=download&id=1IJMtxqsBg5nxjw-JXdJTe92H_1P6ViM-";

    public Text questionText;
    public Button choiceA;
    public Button choiceB;
    public Button choiceC;
    public Button choiceD;
    public Text feedbackText;

    private RootData jsonData;
    private int currentQuestionIndex;

    private void Awake()
    {
        choiceA.onClick.AddListener(()=> CheckAnswer("A", choiceA));
        choiceB.onClick.AddListener(() => CheckAnswer("B", choiceB));
        choiceC.onClick.AddListener(() => CheckAnswer("C", choiceC));
        choiceD.onClick.AddListener(() => CheckAnswer("D", choiceD));
    }
    private void Start()
    {
        LoadData();
        
        
    }
    public void LoadData()
    {
        StartCoroutine(FetchJSON());
        //jsonData = JsonUtility.FromJson<RootData>(jsonFile.text);
    }
    IEnumerator FetchJSON()
    {
        UnityWebRequest www = UnityWebRequest.Get(jsonUrl);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            // JSON data is in www.downloadHandler.text
            string jsonText = www.downloadHandler.text;
            Debug.Log(jsonText);
            // You can now parse the JSON data as needed
            ProcessJSON(jsonText);
            ShowNextQuestion();
        }
    }

    void ProcessJSON(string json)
    {
        jsonData = jsonData = JsonUtility.FromJson<RootData>(json);
        // Parse and use the JSON data here
        // For parsing, you can use Unity's built-in JSONUtility or third-party libraries like Newtonsoft.Json.
    }
    public void ShowNextQuestion()
    {
        if (jsonData != null)
        {
            if (currentQuestionIndex < jsonData.data.Count)
            {
                QuestionData currentQuestion = jsonData.data[currentQuestionIndex];
                questionText.text = currentQuestion.question + "\n" +
                   "A. " + currentQuestion.choices.A + "\n" +
                   "B. " + currentQuestion.choices.B + "\n" +
                   "C. " + currentQuestion.choices.C + "\n" +
                   "D. " + currentQuestion.choices.D;
                feedbackText.text = "";
            }
            else
            {
                questionText.text = "Quiz completed!";
                choiceA.gameObject.SetActive(false);
                choiceB.gameObject.SetActive(false);
                choiceC.gameObject.SetActive(false);
                choiceD.gameObject.SetActive(false);
            }
        }
    }

    public void CheckAnswer(string selectedChoice,Button ClickedButton)
    {
        Debug.Log(selectedChoice);  
        if (currentQuestionIndex < jsonData.data.Count)
        {
            QuestionData currentQuestion = jsonData.data[currentQuestionIndex];
            if (selectedChoice == currentQuestion.answer)
            {
                feedbackText.text = "Correct!";
                Debug.Log("correct");
                ClickedButton.GetComponent<Image>().color = Color.green;
            }
            else
            {
                Debug.Log("Incorrect. The correct answer is " + currentQuestion.answer_text);
                feedbackText.text = "Incorrect. The correct answer is " + currentQuestion.answer_text;
                ClickedButton.GetComponent<Image>().color = Color.red;
                switch (currentQuestion.answer)
                {
                    case "A":choiceA.GetComponent<Image>().color = Color.green;
                        break;
                    case "B": choiceB.GetComponent<Image>().color = Color.green;
                        break;
                    case "C": choiceC.GetComponent<Image>().color = Color.green;
                        break;
                    case "D": choiceD.GetComponent<Image>().color = Color.green;
                        break;
                }
            }

            // Move to the next question
            StartCoroutine(Delay());
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5f);
        currentQuestionIndex++;
        ShowNextQuestion();
        choiceD.GetComponent<Image>().color = Color.white;
        choiceA.GetComponent<Image>().color = Color.white;
        choiceB.GetComponent<Image>().color = Color.white;
        choiceC.GetComponent<Image>().color = Color.white;
    }
}
