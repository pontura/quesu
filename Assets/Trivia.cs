using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trivia : MonoBehaviour
{
	public TriviaPairButtons pairButton;
	public int totalPairs = 10;
	public int separation = 700;
	public Transform container;

	void Start()
	{
		LoopUntilReady ();
	}
	void LoopUntilReady()
	{
		if (Data.Instance.triviaData.loaded) {
			Init ();
			return;
		}
		Invoke ("LoopUntilReady", 0.1f);
	}
	int id = 0;
	public void Init()
	{
		for (int a = 0; a < totalPairs; a++)
			LoadPair ();
		id = 0;
	}
	int pairID;
	public void LoadPair()
	{
		TriviaPairButtons newPairButton = Instantiate (pairButton);
		newPairButton.transform.SetParent (container);
		newPairButton.transform.localScale = Vector3.one;
		newPairButton.transform.localPosition = new Vector3 (separation*pairID, 0, 0);
		ItemData data1 = GetNext ();
		ItemData data2 = GetNext ();
		newPairButton.Init (this, data1, data2);
		pairID++;
	}
	ItemData GetNext()
	{
		if (Data.Instance.triviaData.triviaContent.all.Length <= id)
			id = 0;
		ItemData data =  Data.Instance.triviaData.triviaContent.all[id]; 
		id ++;
		return data;
	}
	public void Next()
	{
		id++;
		iTween.MoveTo(container.gameObject, iTween.Hash(
			"x", id*-separation, 
			"islocal", true,
			"time", 1

		));
	}
}
