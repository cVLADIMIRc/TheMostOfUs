using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
	public void exitInMenu()
	{
		SceneManager.LoadScene(0);
	}
}
