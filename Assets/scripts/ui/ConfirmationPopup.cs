using UnityEngine;

public class ConfirmationPopup : MonoBehaviour
{
    [SerializeField]  GameObject panel;
    [SerializeField] ButtonUI submitBtn;
    [SerializeField] ButtonUI closeBtn;
    [SerializeField] TMPro.TMP_Text field;
    System.Action<bool> OnDone;

    void Start()
    {
        submitBtn.Init(OnClicked);
        closeBtn.Init(OnClose);

        Events.OnConfirmationPopup += OnConfirmationPopup;
        panel.SetActive(false);
    }
    void OnDestroy()
    {
        Events.OnConfirmationPopup -= OnConfirmationPopup;
    }
    void OnConfirmationPopup(string text, System.Action<bool> OnDone)
    {
        this.OnDone = OnDone;
        panel.SetActive(true);
        field.text = text;
        submitBtn.SetText("SALIR");
    }
    void OnClicked(ButtonUI b)
    {
        OnDone(true);
        panel.SetActive(false);
    }
    void OnClose(ButtonUI b)
    {
        OnDone(false);
        panel.SetActive(false);
    }
}
