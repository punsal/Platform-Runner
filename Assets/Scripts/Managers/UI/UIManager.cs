using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text runnerText, gameOverText, instructionsText;
    [Range(0, 2)]
    public float blinkTime; 

    private void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        gameOverText.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GameEventManager.TriggerGameStart();
            StopAllCoroutines();
        }
    }

    private void GameStart()
    {
        runnerText.enabled = false;
        gameOverText.enabled = false;
        instructionsText.enabled = false;

        enabled = false;
    }

    private void GameOver()
    {
        gameOverText.enabled = true;
        instructionsText.enabled = true;
        StartCoroutine(OffBlinkInstructions());

        enabled = true;
    }

    private IEnumerator OffBlinkInstructions()
    {
        yield return new WaitForSeconds(blinkTime);
        instructionsText.enabled = false;
        StartCoroutine(OnBlinkInstructions());
    }

    private IEnumerator OnBlinkInstructions()
    {
        yield return new WaitForSeconds(blinkTime);
        instructionsText.enabled = true;
        StartCoroutine(OffBlinkInstructions());
    }
}
