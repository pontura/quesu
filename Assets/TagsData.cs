using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TagsData : MonoBehaviour
{
	public Tags tags;
	public bool loaded;

	[Serializable]
	public class Tags
	{
		public TagData[] all;
	}

	public void SetData(Tags data)
	{
		tags = data;
		loaded = true;
	}
}
