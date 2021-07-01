using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class bank : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayBalance;
    [SerializeField] int startingBalance = 150;
    int currentBalance;
    public int CurrentBalance 
    {
        get{
            return currentBalance;
        }
    }
    void Awake() {
        currentBalance = startingBalance;
        updateDisplay();
    }
    void updateDisplay()
    {
        displayBalance.text = "Gold : " + currentBalance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        updateDisplay();
    }
    public void withDraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        updateDisplay();
        if(currentBalance<0)
        {
            ReloadScene();
        }
    }
    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
