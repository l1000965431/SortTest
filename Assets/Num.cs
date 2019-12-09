using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Num : MonoBehaviour
{
    public Text Text_Num;
    public int mNumIndex;
    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (Text_Num != null) { Text_Num.text = mNumIndex.ToString(); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
