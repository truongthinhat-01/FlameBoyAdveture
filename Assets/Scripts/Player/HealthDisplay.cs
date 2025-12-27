using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] GameObject healthIconPregab;
    [SerializeField] List<GameObject>icon;
    [SerializeField] private int point = 0;

    public int Points
    {
        get { return point;}

        set 
        { 
            int oldValue = point;
            point = value;
            ManagerIcons(point - oldValue);
        }
    }
    void ManagerIcons(int deltaPoints)
    {
        
    }
    
}
