using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;

public class LoadXml:MonoBehavior{
	XDocument xmlDoc;
	IEnumerable<XElement> items;
	List<XMLData> data= new List<XMLData>();
		
	int iteration =0, pageNum=0;
	string charText, dialogueText;
	bool finishedLoading=false;
	
	void Start()
	{
		DontDestroyOneLoad(gameObject);
		LoadXML();
		StartCoroutine("AssignData");	
	}
	
	void Update()
	{
		if(finishedLoading)
		{
		Application.LoadLevel("TestScene");
		finishedLoading=false;
		}
	}
	
	void LoadXML()
	{	
		xmlDoc=XDocument.Load("Assets/Resources/XML Files/circles.xml");
		items= xmlDoc.Descendants("pages").Elements();
	}
	
	IEnumerator AssignData()
	{
		foreach(var item in items)
		{
			if(item.Parent.Attribute("number").Value==iteration.ToString())
			{
				pageNum=in.Parse(item.Parent.Attribute("number").Value);
				charText=item.Parent.Element("name").Value.Trim();
				dialogueText=item.Parent.Element("dialogue").Value.Trim();
	
				data.Add(new XMLData(pageNum, charText, dialogueText));
	
				Debug.Log(data[iteration].dialogueText);
				iteration++;
			}
		}
		
		finishedLoading=true;
	}
			
}
