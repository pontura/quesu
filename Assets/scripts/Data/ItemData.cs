using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class ItemData
{
	public string id;
	public string text;
	public string image;
	public int year;
	public string description;
	public Texture2D texture;
	public bool usedInGame;
    public bool textureReady;
    public Texture2D GetImage()
    {
        return texture;
    }
    public void SetTexture2D (Texture2D t)
    {
        textureReady = true;
        texture = t;
    }

}
