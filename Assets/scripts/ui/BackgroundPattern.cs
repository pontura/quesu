using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundPattern : MonoBehaviour
{
    [SerializeField] Sprite sprite;
    [SerializeField] GameObject asset;
    [SerializeField] Transform container;
    [SerializeField] Vector2 separation;
    [SerializeField] int rows = 5;
    [SerializeField] int cols = 10;
    [SerializeField] float speed = 10;

    void Start()
    {
        Draw();
    }
    public void Draw()
    {
        Utils.RemoveAllChildsIn(container);
        for(int a = 0; a<cols; a++)
        {
            for (int b = 0; b < rows; b++)
            {
                GameObject go = Instantiate(asset, container);
                go.transform.localPosition = new Vector2(separation.x*b, separation.y*a);
                asset.GetComponentInChildren<Image>().sprite = sprite;
            }
        }
    }
    float _y = 0;
    private void Update()
    {
        _y += Time.deltaTime * speed;
        if (_y > separation.y)
            _y = 0;
        Vector2 pos = container.transform.localPosition;
        pos.y = _y;
        container.transform.localPosition = pos;
    }
}
