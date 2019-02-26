using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public void PlaySingleMode()
	{
		Data.Instance.LoadLevel ("01_Game");
	}
}
