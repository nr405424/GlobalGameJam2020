﻿#region

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class ScoreSystem : GenericSingleton<ScoreSystem>
{
    #region TIMER

    private void Update()
    {
        int minutes = 0;
        int seconds = 0;
        if (conveyerStopObject)
        {
            if (conveyerStopObject.IsFocused)
            {
                if (comboCountBonus == 3) {
                    //Debug.Log("combo count bonus = 3");
                    if (currentObject != null) {
                        currentObject.GetComponent<Repair>().isRepared = true;
                        currentObject.GetComponent<RotateObject>().autoRepaired = true;
                    }
                    else {
                        Debug.Log("current obj null");
                    }

                }

                if (TimeIsFrozen)
                {
                    if (!isEqualToFreezeTime)
                    {
                        temp += Time.deltaTime;
                        waitTime = Mathf.FloorToInt(temp);

                        if (waitTime >= freezeTime)
                        {
                            isEqualToFreezeTime = true;
                        }
                    }
                    else
                    {
                        TimeIsFrozen = false;
                    }
                }
                else
                {
                    timeSinceObjectInFront += Time.deltaTime;
                    minutes = Mathf.FloorToInt(timeSinceObjectInFront / 60);
                    seconds = Mathf.FloorToInt(timeSinceObjectInFront - minutes * 60);
                    timeLapsed = seconds;
                }
            }
            else
            {
                timeSinceObjectInFront = 0;
                minutes = 0;
                seconds = 0;
                waitTime = 0;
                temp = 0;
                isEqualToFreezeTime = false;
            }
        }

        TimeTextField.text = minutes + ":" + seconds;
        ScoreTextField.text = "" + score;
    }

    #endregion

    #region VARIABLES

    public TextMeshProUGUI TimeTextField;
    public TextMeshProUGUI ScoreTextField;

    private static int score;
    private float timeSinceObjectInFront;
    private float timeLapsed;
    public int freezeTime;

    [SerializeField] private bool timeIsFrozen;

    private bool isEqualToFreezeTime;
    private float temp;
    private int waitTime;

    private int comboCountBonus = 0;
    private int comboCountMalus = 0;
    private bool canMakeComboBonus = true;
    private bool canMakeComboMalus = true;
    private bool isDoubleScore = false;
    [HideInInspector]
    public GameObject currentObject;


    [SerializeField] private ConveyerStopObject conveyerStopObject;

    #endregion

    #region SCORE

    public int SetScore(GameObject currentObject, float currentTimeLapsed, out int freezeSeconds,
        out bool secondsFrozen)
    {
        switch (currentObject.GetComponent<RotateObject>().difficulty)
        {
            case ObjectDifficulty.easy:
                if (currentTimeLapsed <= 10)
                {
                    score += 100;
                    if (comboCountBonus == 2) {
                        score += 100;
                    } 
                    //else if (comboCountBonus == 3) {
                    //    gameObject.GetComponent<Repair>().isRepared = true;
                    //    gameObject.GetComponent<RotateObject>().validate = true;

                    //}
                    canMakeComboBonus = false;
                }
                else if (currentTimeLapsed <= 12)
                {
                    score += 75;
                    if (comboCountBonus == 2) {
                        score += 75;
                    } else if (comboCountBonus == 3) {
                        //gameObject.GetComponent<Repair>().isRepared = true;
                        //gameObject.GetComponent<RotateObject>().validate = true;
                        score += 25;
                    }
                    canMakeComboBonus = false;
                }
                else if (currentTimeLapsed <= 15)
                {
                    score += 50;
                    if (comboCountBonus == 2) {
                        score += 50;
                    } else if (comboCountBonus == 3) {
                        //gameObject.GetComponent<Repair>().isRepared = true;
                        //gameObject.GetComponent<RotateObject>().validate = true;
                        score += 50;
                    }
                    canMakeComboBonus = false;
                }
                else
                {
                    score += 25;
                    freezeSeconds = 3;
                    secondsFrozen = true;
                    if (canMakeComboBonus) {
                        if (comboCountBonus == 2) {
                            score += 25;
                        } else if (comboCountBonus == 3) {
                            //gameObject.GetComponent<Repair>().isRepared = true;
                            //gameObject.GetComponent<RotateObject>().validate = true;
                            score += 75;
                        }
                        comboCountBonus++;
                    }
                    canMakeComboBonus = true;
                    return score;
                }

                freezeSeconds = 0;
                secondsFrozen = false;
                return score;
            case ObjectDifficulty.normal:
                if (currentTimeLapsed <= 15)
                {
                    score += 100;
                    if (comboCountBonus == 2) {
                        score += 100;
                    } 
                    //else if (comboCountBonus == 3) {
                    //    //gameObject.GetComponent<Repair>().isRepared = true;
                    //    //gameObject.GetComponent<RotateObject>().validate = true;
                    //}
                    canMakeComboBonus = false;
                }
                else if (currentTimeLapsed <= 18)
                {
                    score += 75;
                    if (comboCountBonus == 2) {
                        score += 75;
                    } else if (comboCountBonus == 3) {
                        //gameObject.GetComponent<Repair>().isRepared = true;
                        //gameObject.GetComponent<RotateObject>().validate = true;
                        score += 25;
                    }
                    canMakeComboBonus = false;
                }
                else if (currentTimeLapsed <= 22)
                {
                    score += 50;
                    if (comboCountBonus == 2) {
                        score += 50;
                    } else if (comboCountBonus == 3) {
                        //gameObject.GetComponent<Repair>().isRepared = true;
                        //gameObject.GetComponent<RotateObject>().validate = true;
                        score += 50;
                    }
                    canMakeComboBonus = false;
                }
                else
                {
                    score += 25;
                    freezeSeconds = 5;
                    secondsFrozen = true;
                    if (canMakeComboBonus) {
                        if (comboCountBonus == 2) {
                            score += 25;
                        } else if (comboCountBonus == 3) {
                            //gameObject.GetComponent<Repair>().isRepared = true;
                            //gameObject.GetComponent<RotateObject>().validate = true;
                            score += 75;
                        }
                        comboCountBonus++;
                    }
                    canMakeComboBonus = true;
                    return score;
                }

                freezeSeconds = 0;
                secondsFrozen = false;
                return score;
            case ObjectDifficulty.hard:
                if (currentTimeLapsed <= 20)
                {
                    score += 100;
                    if (comboCountBonus == 2) {
                        score += 100;
                    }
                    //else if (comboCountBonus == 3) {
                    //    gameObject.GetComponent<Repair>().isRepared = true;
                    //    gameObject.GetComponent<RotateObject>().validate = true;
                    //}
                    canMakeComboBonus = false;
                }
                else if (currentTimeLapsed <= 22)
                {
                    score += 75;
                    if (comboCountBonus == 2) {
                        score += 75;
                    } else if (comboCountBonus == 3) {
                        //gameObject.GetComponent<Repair>().isRepared = true;
                        //gameObject.GetComponent<RotateObject>().validate = true;
                        score += 25;
                    }
                    canMakeComboBonus = false;
                }
                else if (currentTimeLapsed <= 26)
                {
                    score += 50;
                    if (comboCountBonus == 2) {
                        score += 50;
                    } else if (comboCountBonus == 3) {
                        //gameObject.GetComponent<Repair>().isRepared = true;
                        //gameObject.GetComponent<RotateObject>().validate = true;
                        score += 50;
                    }
                    canMakeComboBonus = false;
                }
                else
                {
                    score += 25;
                    freezeSeconds = 7;
                    secondsFrozen = true;
                    if (canMakeComboBonus) {
                        if (comboCountBonus == 2) {
                            score += 25;
                        } else if (comboCountBonus == 3) {
                            //gameObject.GetComponent<Repair>().isRepared = true;
                            //gameObject.GetComponent<RotateObject>().validate = true;
                            score += 75;
                        }
                        comboCountBonus++;
                    }
                    canMakeComboBonus = true;
                    return score;
                }

                freezeSeconds = 0;
                secondsFrozen = false;
                return score;

            default:
                freezeSeconds = 0;
                secondsFrozen = false;
                return 0;
        }
    }

    public float TimeLapsed => timeLapsed;

    public int Score
    {
        get => score;
        set
        {
            if (value >= 0)
            {
                score = value;
            }
        }
    }

    public bool TimeIsFrozen
    {
        get => timeIsFrozen;
        set => timeIsFrozen = value;
    }

    #endregion
}