using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour {
    // HingeJointコンポーネントを入れる
    private HingeJoint myHingeJoint;

    // 初期の傾き
    private float defaultAngle = 20;
    // 弾いた時の傾き
    private float flickAngle = -20;

    // 画面左をタップした指のID
    private int LeftFingerId = -1;
    // 画面右をタップした指のID
    private int RightFingerId = -1;

	// Use this for initialization
	void Start () {
		// HingeJointコンポーネント取得
        this.myHingeJoint = GetComponent<HingeJoint>();

        // フリッパーの傾きを設定
        SetAngle(this.defaultAngle);
	}
	
	// Update is called once per frame
	void Update () {
		
        // 左矢印キーを押した時左フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.LeftArrow) && tag == "LeftFripperTag") {
            SetAngle(this.flickAngle);
        }
        // 右矢印キーを押した時右フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.RightArrow) && tag == "RightFripperTag") {
            SetAngle(this.flickAngle);
        }

        // 矢印キーが離された時フリッパーを元に戻す
        if (Input.GetKeyUp(KeyCode.LeftArrow) && tag == "LeftFripperTag") {
            SetAngle(this.defaultAngle);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && tag == "RightFripperTag") {
            SetAngle(this.defaultAngle);
        }

        // タップの情報を取得
        Touch[] touches=Input.touches;
        // 押されてる指の数だけ処理する
        for (int i = 0; i < Input.touchCount; i++) {
            // 画面左側をタップされた時左フリッパーを動かす
            if (touches[i].phase == TouchPhase.Began && touches[i].position.x <= Screen.width / 2 && this.LeftFingerId == -1 && tag == "LeftFripperTag") {
                SetAngle(this.flickAngle);
                // 左フリッパーを動かした指を記録する
                this.LeftFingerId = touches[i].fingerId;
            }
            // 画面右側をタップされた時右フリッパーを動かす
            if (touches[i].phase == TouchPhase.Began && touches[i].position.x > Screen.width / 2 && this.RightFingerId == -1 && tag == "RightFripperTag") {
                SetAngle(this.flickAngle);
                // 右フリッパーを動かした指を記録する
                this.RightFingerId = touches[i].fingerId;
            }

            // 指が離された時フリッパーを元に戻す
            if (touches[i].phase == TouchPhase.Ended && touches[i].fingerId == this.LeftFingerId) {
                SetAngle(this.defaultAngle);
                this.LeftFingerId = -1;
            }
            if (touches[i].phase == TouchPhase.Ended && touches[i].fingerId == this.RightFingerId) {
                SetAngle(this.defaultAngle);
                this.RightFingerId = -1;
            }
        }
	}

    // フリッパーの傾きを設定
    private void SetAngle(float angle) {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }
}
