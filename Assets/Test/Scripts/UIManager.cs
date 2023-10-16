using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] List<GameObject> panels = new List<GameObject>();
    GameManager gameManager;
    
    void Start()
    {
        if (GameObject.FindWithTag("GameManager").gameObject.GetComponent<GameManager>() != null)
        {
            gameManager = GameObject.FindWithTag("GameManager").gameObject.GetComponent<GameManager>();
        }
    }
    void Update()
    {
        
    }

    public void EnablePanel(string name)
    {
        foreach (GameObject panel in panels)
        {
           panel.SetActive((panel.name==name)?true:false);
        }
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1f;
    }
    public void OnPause()
    {
        Time.timeScale = 0f;
        gameManager.DistanceUpdate();
    }
    public void OnResume()
    {
        Time.timeScale = 1f;
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void ButtonMapping(string name)
    {
        EnablePanel(name);
    }
    public void Exitbutton()
    {
        Application.Quit();
    }
}
