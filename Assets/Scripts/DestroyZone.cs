/*-------------------------------------
消滅範囲の管理(DestroyZone.cs)

date  :2017.03.28
Author:Miyu Hara
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour {
    /* 関数の定義 */
    /*------------------------------------
    OnTriggerEnter2D
    
    summary:トリガー処理
    param  :衝突したデータ(Collision2D)
    return :なし(void)
    ------------------------------------*/
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
            Destroy(coll.gameObject);
    }
}
