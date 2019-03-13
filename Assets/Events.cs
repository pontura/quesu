using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {
	public static System.Action<string> OnSoundFX = delegate { };
	public static System.Action<ButtonStandard> OnButtonClicked = delegate { };
	public static System.Action<bool> OnAnswer = delegate { };
	public static System.Action OnResetTrivia = delegate { };
	public static System.Action<int> OnCombo = delegate { };
	public static System.Action<string> OnServerResponse = delegate { };
}
