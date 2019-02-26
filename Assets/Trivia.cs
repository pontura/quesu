﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trivia : MonoBehaviour
{
	public TriviaPairButtons pairButton;
	int totalPairs = 3;
	public int serieID = 0;
	int separationY = 148;
	int separation = 370;
	public Transform container;
	public TimerManager timerManager;
	int itemId;
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
		timerManager.Init (60);
	}
	public void Init()
	{
		LoadNewSerie ();
		serieID++;
	}
	public void LoadNewSerie()
	{
		Utils.RemoveAllChildsIn (container);
		container.transform.localPosition = new Vector2(0,container.transform.localPosition.y);
		StartCoroutine (LoadRoutine ());
	}
	IEnumerator LoadRoutine()
	{
		pairID = 0;
		for (int a = 0; a < totalPairs; a++) {
			LoadPair ();
			yield return new WaitForSeconds (0.2f);
		}
		timerManager.SetState(true);
		yield return null;
	}
	int pairID;
	public void LoadPair()
	{
		TriviaPairButtons newPairButton = Instantiate (pairButton);
		newPairButton.transform.SetParent (container);
		newPairButton.transform.localScale = Vector3.one;
		newPairButton.transform.localPosition = new Vector3 (0, -separationY*pairID, 0);
		ItemData data1 = GetNext ();
		ItemData data2 = GetNext ();
		newPairButton.Init (this, data1, data2);
		pairID++;
	}
	ItemData GetNext()
	{
		if (Data.Instance.triviaData.triviaContent.all.Length <= itemId)
			itemId = 0;
		ItemData data =  Data.Instance.triviaData.triviaContent.all[itemId]; 
		itemId ++;
		return data;
	}
	int pairDone = 0;
	public void PairDone()
	{
		pairDone++;
		if (pairDone >= totalPairs) {
			timerManager.SetState(false);
			Invoke("Next", 2);
			pairDone = 0;
		}
	}
	void Next()
	{
		
		iTween.MoveTo(container.gameObject, iTween.Hash(
			"x", -separation, 
			"islocal", true,
			"time", 1,
			"oncomplete", "OnAnimationReady",
			"oncompletetarget", this.gameObject
		));
	}
	void OnAnimationReady()
	{
		Invoke("Init", 0.1f);
	}
}
