﻿using UnityEngine;
using System.Collections;

public class SpellManager : MonoBehaviour {

    // WARNING: NO LONGER USED

	Animator anim;


	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			anim.SetTrigger("AttackHab01");
		}
	
	}
}
