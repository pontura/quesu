using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonUI : MonoBehaviour
{
    public TMPro.TMP_Text field;
    System.Action<int> OnClick;

    public void Init(System.Action<ButtonUI> OnClick)
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => { OnClick(this); });
    }
    public void SetText(string s)
    {
        field.text = s;
    }
}
