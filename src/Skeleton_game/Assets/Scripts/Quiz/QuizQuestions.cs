/**
*
*   Script Description: This script has the purpose of controlling the Quiz Points.
*                       Out of a set of predefined questions, 1 is randomly selected and presented to the player.
*                       This also determines if the player's answer is correct. If so a reward is randomly given.
*
*   Author: Daniel Sousa
*
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuizQuestions : MonoBehaviour
{

    public enum questionId {Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Q11,Q12,Q13,Q14,Q15,Q16,Q17,Q18}
    public bool answered = false;
    [HideInInspector]
    public questionId question;
    

    string questionTitle;
    string A;
    string B;
    string C;
    string D;
    public string correctAnswer;
    public bool answeredCorrectly;
    string reward;



    void Start()
    {
        questionId value = RandomEnumValue<questionId>();
        setupQuestion(value);

    }

    static System.Random random = new System.Random();
    static T RandomEnumValue<T> ()
    {
        var v = Enum.GetValues (typeof (T));
        return (T) v.GetValue (random.Next(v.Length));
    }


    void setupQuestion(questionId option) {
        switch (option)
        {
            case questionId.Q1:
                questionTitle = "When was the university founded?";
                A = "1999";
                B = "2003";
                C = "2008";
                D = "1990";
                correctAnswer = B;
                break;

            case questionId.Q2:
                questionTitle = "What are the acronyms of the 3 faculties of Luxembourg?";
                A = "FSTM, FMRE, FDEF";
                B = "FMT, FSEM, FRME";
                C = "FHSE, FSTM, FDEF";
                D = "FHSE, FMT, FDEF";
                correctAnswer = C;
                break;
            case questionId.Q3:
                questionTitle = "Who's the Head of the Computer Science Department?";
                A = "Pascal Bouvry";
                B = "Sandra Rosin";
                C = "Volker Müller";
                D = "Christoph Schommer";
                correctAnswer = A;
                break;
            case questionId.Q4:
                questionTitle = "How many bachelors does the FSTM offer?";
                A = "3";
                B = "5";
                C = "7";
                D = "8";
                correctAnswer = D;
                break;
            case questionId.Q5:
                questionTitle = "Who's the Head of the Engineering Department?";
                A = "Pascal Bouvry";
                B = "Sjouke Mauw";
                C = "Volker Müller";
                D = "Stephan Leyer";
                correctAnswer = D;
                break;
            case questionId.Q6:
                questionTitle = "Who's the Head of the Mathematics Department?";
                A = "Elisabeth Alves";
                B = "Sjouke Mauw";
                C = "Giovanni Peccati";
                D = "Stephan Leyer";
                correctAnswer = C;
                break;
            case questionId.Q7:
                questionTitle = "Who's the Head of the Physics Department?";
                A = "Elisabeth Alves";
                B = "Alexandre Tkatchenko";
                C = "Yves Le Traon";
                D = "Christian Grevisse";
                correctAnswer = B;
                break;
            case questionId.Q8:
                questionTitle = "Who's the Head of the Physics Department?";
                A = "Iris Behrmann";
                B = "Christian Grevisse";
                C = "Yves Le Traon";
                D = "Steffen Rothkugel";
                correctAnswer = A;
                break;
            case questionId.Q9:
                questionTitle = "Which of these is not a campus of the University of Luxembourg?";
                A = "Belval Campus";
                B = "Kirchberg Campus";
                C = "Diekirch Campus";
                D = "Limpertsberg Campus";
                correctAnswer = C;
                break;
            case questionId.Q10:
                questionTitle = "Which of the following bachelors does NOT belong to the FSTM?";
                A = "Biology and Medicine";
                B = "Physics";
                C = "Computer Science";
                D = "Economics and Management";
                correctAnswer = D;
                break;
            case questionId.Q11:
                questionTitle = "Whats the maximum number of languages used to teach students in FSTM bachelors?";
                A = "1";
                B = "2";
                C = "3";
                D = "4";
                correctAnswer = C;
                break;
            case questionId.Q12:
                questionTitle = "How many Global Exchange Program partner universities does the University of Luxembourg have?";
                A = "7";
                B = "45";
                C = "30";
                D = "62";
                correctAnswer = B;
                break;
            case questionId.Q13:
                questionTitle = "Which of these acronyms is NOT a masters in Mathematics?";
                A = "MMAI";
                B = "MMATH";
                C = "MSE-MATH";
                D = "MADS";
                correctAnswer = A;
                break;
            case questionId.Q14:
                questionTitle = "How many eating facilities are there in Belval Campus?";
                A = "4";
                B = "3";
                C = "2";
                D = "1";
                correctAnswer = A;
                break;
            case questionId.Q15:
                questionTitle = "Which of the following is NOT an eating facilities in Belval Campus?";
                A = "Food Café";
                B = "Food House";
                C = "Food Lab";
                D = "Food Area";
                correctAnswer = D;
                break;
            case questionId.Q16:
                questionTitle = "To which faculty does the 'Bachelor en Gestion' belong to?";
                A = "FSTM";
                B = "FDEF";
                C = "FHSE";
                D = "None of the above";
                correctAnswer = B;
                break;
            case questionId.Q17:
                questionTitle = "To which faculty does the 'Bachelor in Applied Information Technology' belong to?";
                A = "FSTM";
                B = "FDEF";
                C = "FHSE";
                D = "None of the above";
                correctAnswer = A;
                break;
            case questionId.Q18:
                questionTitle = "To which faculty does the 'Bachelor of Science in Psychology' belong to?";
                A = "FSTM";
                B = "FDEF";
                C = "FHSE";
                D = "None of the above";
                correctAnswer = C;
                break;

            default:
                break;
        }

        TMPro.TextMeshProUGUI title;
        Text Atext;
        Text Btext;
        Text Ctext;
        Text Dtext;
        title = gameObject.transform.Find("Dialogue Panel").Find("Text").GetComponent<TMPro.TextMeshProUGUI>();
        Atext = gameObject.transform.Find("Buttons").Find("Item1").Find("DescriptionText").GetComponent<Text>();
        Btext = gameObject.transform.Find("Buttons").Find("Item2").Find("DescriptionText").GetComponent<Text>();
        Ctext = gameObject.transform.Find("Buttons").Find("Item3").Find("DescriptionText").GetComponent<Text>();
        Dtext = gameObject.transform.Find("Buttons").Find("Item4").Find("DescriptionText").GetComponent<Text>();
        title.text = questionTitle;
        Atext.text = A;
        Btext.text = B;
        Ctext.text = C;
        Dtext.text = D;
    }

    public void GoodAnswer() {
        SelectReward();
        gameObject.transform.Find("RewardPanel").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Congrats! You won " + reward + "!";
    }

    public void BadAnswer() {
        gameObject.transform.Find("RewardPanel").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Incorrect! Learn more at:\nhttps://www.uni.lu";
    }

    public void SelectReward() {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        healthSystem hs = player.GetComponent<healthSystem>();
        int rd = UnityEngine.Random.Range(1,101);
        if (rd <= 20)
        {
            //Give player 150 coins
            PlayerMoney.money += 150;
            reward = "150 coins";

        } else if (rd > 20 && rd <= 40)
        {
            //Give player 50 health
            if(hs.health + 50 >=  hs.maxHealth)
            {
                hs.shield = hs.shield + 50 - hs.maxHealth + hs.health;
                hs.health = hs.maxHealth;
                if (hs.shield >= hs.maxHealth)
                {
                    hs.shield = hs.maxHealth;
                }
            }
            else
            {
                hs.health += 50;
            }
            hs.healthBar.setHealth(hs.health);
            hs.shieldBar.setShield(hs.shield);
            reward = "50 health bonus";
            
        } else if (rd > 40 && rd <= 55)
        {
            //Give player 250 coins
            PlayerMoney.money += 250;
            reward = "250 coins";

        } else if (rd > 55 && rd <= 70)
        {
            //Give player 100 health
            if(hs.health + 100 >=  hs.maxHealth)
            {
                hs.shield = hs.shield + 100 - hs.maxHealth + hs.health;
                hs.health = hs.maxHealth;
                if (hs.shield >= hs.maxHealth)
                {
                    hs.shield = hs.maxHealth;
                }
            }
            else
            {
                hs.health += 100;
            }
            hs.healthBar.setHealth(hs.health);
            hs.shieldBar.setShield(hs.shield);
            reward = "100 health bonus";

        } else if (rd > 70 && rd <= 80)
        {
            //Give player 400 coins
            PlayerMoney.money += 400;
            reward = "400 coins";

        } else if (rd > 80 && rd <= 90)
        {
            //Give player 150
            if(hs.health + 150 >=  hs.maxHealth)
            {
                hs.shield = hs.shield + 150 - hs.maxHealth + hs.health;
                hs.health = hs.maxHealth;
                if (hs.shield >= hs.maxHealth)
                {
                    hs.shield = hs.maxHealth;
                }
            }
            else
            {
                hs.health += 150;
            }
            hs.healthBar.setHealth(hs.health);
            hs.shieldBar.setShield(hs.shield);
            reward = "150 health bonus";

        } else if (rd > 90 && rd <= 95)
        {
            //Give player 500 coins
            PlayerMoney.money += 500;
            reward = "500 coins";

        } else if (rd > 95 && rd <= 100)
        {
            //Give player 250 health
            if(hs.health + 250 >=  hs.maxHealth)
            {
                hs.shield = hs.shield + 250 - hs.maxHealth + hs.health;
                hs.health = hs.maxHealth;
                if (hs.shield >= hs.maxHealth)
                {
                    hs.shield = hs.maxHealth;
                }
            }
            else
            {
                hs.health += 250;
            }
            hs.healthBar.setHealth(hs.health);
            hs.shieldBar.setShield(hs.shield);
            reward = "250 health bonus";
        }

        Damage.lostShield = hs.maxShield - hs.shield;
        Damage.lostHealth = hs.maxHealth - hs.health;
    }

    public void terminateQuiz() {
        if (answered)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
