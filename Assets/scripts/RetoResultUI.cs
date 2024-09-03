using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetoResultUI : MonoBehaviour
{
    public GameObject panel;
    public Text title;
    public Text field;

    void Start()
    {
        panel.SetActive(false);
    }
    public void Init(int total)
    {
        panel.SetActive(true);
        if (total > Data.Instance.retosManager.openReto.score)
        {
            title.text = "¡Ganaste!";
        }  else if (total == Data.Instance.retosManager.openReto.score)
        {
            title.text = "¡Empate!";
        }
        else
        {
            title.text = "¡Perdiste!";
        }

        field.text = Data.Instance.retosManager.openReto.username + " hizo: " + Data.Instance.retosManager.openReto.score + " puntos.";
        Invoke("Reset", 3);
    }
    void Reset()
    {
        panel.SetActive(false);
    }
}
