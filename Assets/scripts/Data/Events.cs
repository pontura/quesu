using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {

    public static System.Action OnUserReady = delegate { };

    public static System.Action<bool> OnChooseLeft = delegate { };
    public static System.Action<bool> OnAnswer = delegate { };
    public static System.Action<int> OnChangeTag = delegate { };
    public static System.Action OnResetTrivia = delegate { };
	public static System.Action<int> OnCombo = delegate { };
	public static System.Action<string> OnServerResponse = delegate { };
	public static System.Action<string> OnPopup = delegate { };
    public static System.Action<string, System.Action<bool>> OnConfirmationPopup = delegate { };
    public static System.Action CloseConfirmationPopup = delegate { };
    public static System.Action OnRetoResultShow = delegate { };

	public static System.Action<string> OnMusic = delegate { };
	public static System.Action<string> OnSoundFX = delegate { };
	public static System.Action<string> OnUIFX = delegate { };

	public static System.Action OnInitRonda = delegate { };
    public static System.Action OnTimeOver = delegate { };


}
