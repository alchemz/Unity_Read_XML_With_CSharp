using UnityEngine;
using System.Collections;
using System.Collections.Generic; //Needed for Lists
using System.Xml; //Needed for XML functionality
using System.Xml.Serialization; //Needed for XML Functionality
using System.IO;
using System.Xml.Linq; //Needed for XDocument



public class Loader : MonoBehaviour {

	XDocument xmlDoc; 
	IEnumerable<XElement> items; 
	List <XMLData> data = new List <XMLData>(); 
	int iteration = 0, pageNum = 0;
	string charText, dialogueText;
	bool finishedLoading = false;

	void Start ()
	{
		DontDestroyOnLoad (gameObject);
		LoadXML(); 
		StartCoroutine (AssignData()); 
	}

	void Update ()
	{
		if (finishedLoading)
		{
			Application.LoadLevel ("TestScene");
			finishedLoading = false;
		}
	}

	void LoadXML()

	{

		//Assigning Xdocument xmlDoc. Loads the xml file from the file path listed. 
		xmlDoc = XDocument.Load("Assets/Resources/circle.xml");

		//This basically breaks down the XML Document into XML Elements. Used later. 
		items = xmlDoc.Descendants("page").Elements ();

	}

	//this is our coroutine that will actually read and assign the XML data to our List 
	IEnumerator AssignData()

	{

		foreach (var item in items)
		{
			if(item.Parent.Attribute("number").Value == iteration.ToString ())
			{
				pageNum = int.Parse (item.Parent.Attribute ("number").Value); 
				charText = item.Parent.Element("name").Value.Trim (); 
				dialogueText = item.Parent.Element ("dialogue").Value.Trim ();
				data.Add (new XMLData(pageNum, charText, dialogueText));

				Debug.Log (data[iteration].dialogueText);
				iteration++; 
			}
		}

		finishedLoading = true; //tell the program that we’ve finished loading data. yield return null;

	}

}

// This class is used to assign our XML Data to objects in a list so we can call on them later.
public class XMLData {

	public int pageNum;

	public string charText, dialogueText;

	// Create a constructor that will accept multiple arguments that can be assigned to our variables. 
	public XMLData (int page, string character, string dialogue)

	{

		pageNum = page;

		charText = character;

		dialogueText = dialogue;

	}

}
