using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {
	public List<Text> scoreText;

	private uint[] score;

	// Use this for initialization
	void Start () {
		score = new uint[2];
		score [0] = 0;
		score [1] = 0;

		scoreText[0].text = "P1: " + score [0];
		scoreText[1].text = "P2: " + score [1];
	}

	public void IncScore(uint player) {
		++score [player];
		scoreText [(int)player].text = "P" + (player + 1) + ": " + score [player];
	}

	public uint GetScore(uint player) {
		return score [player];
	}
}
