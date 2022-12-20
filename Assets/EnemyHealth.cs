using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float Enemyhealth = 2;
    private void Update()
    {
        if(Enemyhealth<=0)
        {
            Destroy(gameObject);
            ui_Manager Ui = GetComponent<ui_Manager>();
            Ui.DisplayGameOverCanvas();
        }
    }
}
