using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropViewer : MonoBehaviour
{
    public GameObject[] props;

	int selection;

	void Start()
	{
		selection = 0;
	}
	/*
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if(selection < props.size() - 1);
				selection++;
		}
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			if(selection > 0)
			{
				selection--;
			}
		}
	}
	*/
}
