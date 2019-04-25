using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndCard : MonoBehaviour
{
    [SerializeField] private float timer;

    [SerializeField] private Text timeText;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        LevelManager.OnLevelComplete += DisplayEndCard;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelComplete -= DisplayEndCard;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    public void DisplayEndCard()
    {
        anim.SetTrigger("fadein");
        timeText.text = GetCompleteTime();
    }

    public string GetCompleteTime()
    {
        string minutes = "";
        int secondsInInt;
        string seconds = "";

        minutes += (((int)timer) / 60).ToString();
        secondsInInt = (int) timer % 60;
        if (secondsInInt < 10)
        {
            seconds = "0" + secondsInInt.ToString();
        } else
        {
            seconds = secondsInInt.ToString();
        }

        return minutes + ":" + seconds;
    }
}
