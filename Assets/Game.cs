using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	static Game mInstance;

	public static Game Instance
	{
		get
		{
			if (mInstance == null)
				mInstance = FindObjectOfType<Game>();
			return mInstance;
		}
	}
	void OnAwake()
	{
		mInstance = this;
	}
}
