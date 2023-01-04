using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
             ResMgr.GetInstance().Load<GameObject>("Cube/Cube");
        }

        if (Input.GetMouseButtonDown(1))
        {
            ResMgr.GetInstance().LoadAsync<GameObject>("Cube/Cube", (obj) =>
            {
                obj.transform.localScale = Vector3.one * 3;
            });
        }
    }

    void dosomething(GameObject obj)
    {   
        
        
    }
}
