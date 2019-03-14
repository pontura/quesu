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
	public string GetTitleById(int id)
	{
		foreach(TagData data in tags.all)
		{
			if(data.id == id)
				return data.name;
		}
		return "";
	}
}
