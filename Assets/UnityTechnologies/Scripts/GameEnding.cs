using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [SerializeField] float fadeDuraction = 1f;

    [SerializeField] float displayImageDuration = 1f;

    [SerializeField] GameObject player;

    [SerializeField] CanvasGroup exitBackgroundImageCanvasGroup;

    [SerializeField] CanvasGroup caughtBackgroundImageCanvasGroup;

    [SerializeField] AudioSource exitAudio;

    [SerializeField] AudioSource caughtAudio;

    float m_Timer;

    bool m_IsPlayerExit;

    bool m_IsPlayerCaught;

    bool m_HasAudioPlayed;

    private void Update()
    {
        if (m_IsPlayerExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        
        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerExit = true;
        }
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();

            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;

        imageCanvasGroup.alpha = m_Timer / fadeDuraction;

        if (m_Timer > fadeDuraction + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }
}
