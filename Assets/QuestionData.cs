using System.Collections.Generic;

[System.Serializable]
public class QuestionData
{
    public int ID;
    public string answer;
    public string answer_text;
    public string question;
    public string type;
    public ChoicesData choices;
    public string answer_info;
}
[System.Serializable]
public class ChoicesData
{
    public string A;
    public string B;
    public string C;
    public string D;
}


[System.Serializable]
public class RootData
{
    public List<QuestionData> data;
}

