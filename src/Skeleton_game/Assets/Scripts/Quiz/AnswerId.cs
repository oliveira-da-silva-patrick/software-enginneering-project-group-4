using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerId : MonoBehaviour
{


    public enum Id{ A, B, C, D }

    string rightAnswer;
    string maybeAnswer;

    void Update() {
        var v = gameObject.transform.Find("DescriptionText");
        var t = v.GetComponent<Text>();
        maybeAnswer = t.text;
    }

    public void verifyAnswer() {
        GameObject QuizPanel = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        bool lockquiz = QuizPanel.GetComponent<QuizQuestions>().answered;
        if (!lockquiz)
        {

            rightAnswer = QuizPanel.GetComponent<QuizQuestions>().correctAnswer;
            GameObject rewardPanel = QuizPanel.transform.Find("RewardPanel").Find("RewardText").gameObject;
            if (rightAnswer == maybeAnswer)
            {
                //Call Reward method from 'QuizQuestions'
                QuizPanel.GetComponent<QuizQuestions>().GoodAnswer();
                //Turn button green
                gameObject.GetComponent<Image>().color = new Color32(39,222,68,255);

                //Destroy Quiz Menu & Interaction point on 'Quit'
                //Turning var 'Answered'  to true on the grandparent GameObject will enable the destruction of the corresponding quiz menu and interaction point
                var complete = gameObject.transform.parent.gameObject.GetComponentInParent<QuizQuestions>();
                complete.answered = true;

                //Give player Bonus

                //Destroy Quiz Menu & Interaction point on 'Quit'



            } else
            {
                QuizPanel.GetComponent<QuizQuestions>().BadAnswer();
                gameObject.GetComponent<Image>().color = new Color32(231,15,0,255);
                //Destroy Quiz Menu & Interaction point on 'Quit'
                //Turning var 'answered'  to true on the grandparent GameObject will enable the destruction of the corresponding quiz menu and interaction point
                var complete = gameObject.transform.parent.gameObject.GetComponentInParent<QuizQuestions>();
                complete.answered = true;
            }

        }

    }



}
