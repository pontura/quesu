using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonUI : MonoBehaviour
{
    public TMPro.TMP_Text field;
    System.Action<int> OnClick;
    Button button;
    public void Init(System.Action<ButtonUI> OnClick)
    {
        button = GetComponent<Button>();
        if(button != null)
            button.onClick.AddListener(() => { OnClick(this); });
    }
    private void OnDestroy()
    {
        if (button != null)
            button.onClick.RemoveAllListeners();
    }
    public void SetText(string s)
    {
        field.text = s;
    }
}
