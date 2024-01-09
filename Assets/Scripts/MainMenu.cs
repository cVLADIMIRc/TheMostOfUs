using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioSource audioSource;
	public void Start()
	{
        audioSource = GetComponent<AudioSource>();
        if(audioSource)
        {
            audioSource.Play();
        }
	}
	public void Play(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
