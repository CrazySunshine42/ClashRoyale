
using System;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public partial class MyCardS1
{
		public uint id;

		public string name;

		public string cardPrefab;

		public int[] placeablesIndices;

		public Vector3[] relativeOffsets;


}

[Serializable]
public partial class MyCardModelS1
{
	public List<MyCardS1> list = new List<MyCardS1>();

	public MyCardModelS1()
	{
		list.Add(new MyCardS1(){
			id = 20000,
			name = "Archers",
			cardPrefab = "CardArchersS1",
			placeablesIndices = new []{10000, 10000, 10000},
			relativeOffsets = new []{new Vector3(0.87f, 0f, 0.5f), new Vector3(0f, 0f, 0f), new Vector3(-0.87f, 0f, 0.5f)},
		});

		list.Add(new MyCardS1(){
			id = 20001,
			name = "Mage",
			cardPrefab = "CardMageS1",
			placeablesIndices = new []{10001},
			relativeOffsets = new []{new Vector3(0f, 0f, 0f)},
		});

		list.Add(new MyCardS1(){
			id = 20002,
			name = "Warrior",
			cardPrefab = "CardVikingS1",
			placeablesIndices = new []{10002},
			relativeOffsets = new []{new Vector3(0f, 0f, 0f)},
		});


	}

	public static MyCardModelS1 instance = new MyCardModelS1();
}
