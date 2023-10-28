using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class JSONFetcher : MonoBehaviour
{
    private string jsonUrl = "https://drive.google.com/uc?export=download&id=1ted2BL-9v8PhNcle4n0G1NmpWFmKg9lI";

    public Text questionText;
    public Button choiceA;
    public Button choiceB;
    public Button choiceC;
    public Button choiceD;
    public Text feedbackText;


    public GameObject QUizUI,loading,failed;
    public GameObject GameoverUI;

    private RootData jsonData;
    private int currentQuestionIndex;

    public GameManager gameManager;


    private void Awake()
    {
        choiceA.onClick.AddListener(()=> CheckAnswer("A", choiceA));
        choiceB.onClick.AddListener(() => CheckAnswer("B", choiceB));
        choiceC.onClick.AddListener(() => CheckAnswer("C", choiceC));
        choiceD.onClick.AddListener(() => CheckAnswer("D", choiceD));
        questionText.gameObject.SetActive(false);
        choiceA.gameObject.SetActive(false);
        choiceB.gameObject.SetActive(false);    
        choiceC.gameObject.SetActive(false);
        choiceD.gameObject.SetActive(false);
        feedbackText.gameObject.SetActive(false);
        failed.gameObject.SetActive(false);
    }
    private void Start()
    {
        LoadData();
        loading.SetActive(true);
        if(PlayerPrefs.HasKey("currentQuestionIndex"))
        {
            currentQuestionIndex = PlayerPrefs.GetInt("currentQuestionIndex")+1;
            Debug.Log("Key Exist");
            Debug.Log(PlayerPrefs.GetInt("currentQuestionIndex"));
        }
        else
        {
            PlayerPrefs.SetInt("currentQuestionIndex", 0);
            Debug.Log("Key Not Exist");
        }
      
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
            failed.gameObject.SetActive(true);
            StartCoroutine(FailedDelay());

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
            questionText.gameObject.SetActive(true);
            choiceA.gameObject.SetActive(true);
            choiceB.gameObject.SetActive(true);
            choiceC.gameObject.SetActive(true);
            choiceD.gameObject.SetActive(true);
            feedbackText.gameObject.SetActive(true);
            loading.SetActive(false);
            if (currentQuestionIndex < jsonData.data.Count)
            {

                QuestionData currentQuestion = jsonData.data[currentQuestionIndex];
                questionText.text = currentQuestion.question + "\n" + "\n" +
                   "A. " + currentQuestion.choices.A + "\n" +
                   "B. " + currentQuestion.choices.B + "\n" +
                   "C. " + currentQuestion.choices.C + "\n" +
                   "D. " + currentQuestion.choices.D;
                feedbackText.text = "";
            }
            else
            {
                currentQuestionIndex = 0;
                ShowNextQuestion();
               /* questionText.text = "Quiz completed!";
                choiceA.gameObject.SetActive(false);
                choiceB.gameObject.SetActive(false);
                choiceC.gameObject.SetActive(false);
                choiceD.gameObject.SetActive(false);*/
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
                gameManager.CoinDoubling(2); 
            }
            else
            {
                
                
                ClickedButton.GetComponent<Image>().color = Color.red;
                gameManager.CoinDoubling(1);
                switch (currentQuestion.answer)
                {
                    case "A":
                        {
                            choiceA.GetComponent<Image>().color = Color.green;
                            Debug.Log("Incorrect. The correct answer is " + currentQuestion.choices.A);
                            feedbackText.text = "Incorrect. The correct answer is " + currentQuestion.choices.A;
                        }
                        break;
                    case "B":
                        {
                            choiceB.GetComponent<Image>().color = Color.green;
                            Debug.Log("Incorrect. The correct answer is " + currentQuestion.choices.B);
                            feedbackText.text = "Incorrect. The correct answer is " + currentQuestion.choices.B;
                        }
                        break;
                    case "C":
                        {
                            choiceC.GetComponent<Image>().color = Color.green;
                            Debug.Log("Incorrect. The correct answer is " + currentQuestion.choices.C);
                            feedbackText.text = "Incorrect. The correct answer is " + currentQuestion.choices.C;
                        }
                        break;
                    case "D":
                        {
                            choiceD.GetComponent<Image>().color = Color.green;
                            Debug.Log("Incorrect. The correct answer is " + currentQuestion.choices.D);
                            feedbackText.text = "Incorrect. The correct answer is " + currentQuestion.choices.D;
                        }
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
        PlayerPrefs.SetInt("currentQuestionIndex", currentQuestionIndex);
        GameoverUI.SetActive(true);
       gameManager.UICharacter.SetActive(true);
        //ShowNextQuestion();
        
        choiceD.GetComponent<Image>().color = Color.white;
        choiceA.GetComponent<Image>().color = Color.white;
        choiceB.GetComponent<Image>().color = Color.white;
        choiceC.GetComponent<Image>().color = Color.white;
        //yield return new WaitForSeconds(.5f);
        QUizUI.SetActive(false);
    }
    IEnumerator FailedDelay()
    {
        yield return new WaitForSeconds(2f);
        failed.SetActive(false);
        GameoverUI.SetActive(true);
        QUizUI.SetActive(false);
    }
}
