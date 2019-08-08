using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

    // ボールが見える可能性のあるz軸の最大値
    private float visiblePosZ = -6.5f;

    // ゲームオーバーを表示するテキスト
    private GameObject gameoverText;

    // ゲームの得点
    private int score = 0;
    // ゲームの得点を表示するテキスト
    private GameObject scoreText;

	// Use this for initialization
	void Start () {
		// シーン中のGameOverTextオブジェクトを取得
        this.gameoverText = GameObject.Find("GameOverText");
        // シーン中のScoreTextオブジェクトを取得
        this.scoreText = GameObject.Find("ScoreText");

        // ScoreTextに得点を表示
        this.scoreText.GetComponent<Text>().text = "SCORE：" + this.score.ToString("D5");
	}
	
	// Update is called once per frame
	void Update () {
		// ボールが画面外に出た場合
        if (this.transform.position.z < this.visiblePosZ) {
            // GameOverTextにゲームオーバーを表示
            this.gameoverText.GetComponent<Text>().text = "Game Over";
        }
	}

    // 衝突時に呼ばれる関数
    void OnCollisionEnter(Collision other) {
        // 衝突したオブジェクトによって得点を追加
        if (other.gameObject.tag == "SmallStarTag") {
            // SmallStarは10点追加
            this.score += 10;
        }
        else if (other.gameObject.tag == "LargeStarTag") {
            // LargeStarは20点追加
            this.score += 20;
        }
        else if (other.gameObject.tag == "SmallCloudTag") {
            // SmallCloudは30点追加
            this.score += 30;
        }
        else if (other.gameObject.tag == "LargeCloudTag") {
            // LargeCloudは50点追加
            this.score += 50;
        }
        // ScoreTextに得点を表示
        this.scoreText.GetComponent<Text>().text = "SCORE：" + this.score.ToString("D5");
    }
}
