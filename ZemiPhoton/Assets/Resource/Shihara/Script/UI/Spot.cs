using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

public class Spot : MonoBehaviour {

    private IEnumerator spawnFlow = null;
    private CullingGroup cullingGroup = null;

    [SerializeField]
    Transform[] targetPositions = null;

    [SerializeField]
    GameObject spawnObject = null;
    [SerializeField]
    GameObject spawnObject2 = null;

    GameObject[] Enemy;

    void Awake () {
        spawnFlow = SpawnCoroutine();
    }
	
	
	void OnEnable () {
        //cullinggroupを生成してコルーチン開始
        SetupCullinggroup();
        SetupEnemy();
        StartCoroutine(spawnFlow);
	}

    void OnDisable() {
        //コルーチンを停止してcullinggroupを破棄
        StopCoroutine(spawnFlow);
        cullingGroup.Dispose();
        cullingGroup = null;
    }


    void SetupCullinggroup()
    {
        //culling groupの初期化
        cullingGroup = new CullingGroup();
        cullingGroup.targetCamera = Camera.main;

        //敵を生成する座標の登録
        BoundingSphere[] bounds = new BoundingSphere[targetPositions.Length];
        for(int i = 0; i< targetPositions.Length; i++)
        {
            bounds[i].position = targetPositions[i].position;
            bounds[i].radius = 1;
        }
        cullingGroup.SetBoundingSpheres(bounds);
        cullingGroup.SetBoundingSphereCount(targetPositions.Length);
    }

    void SetupEnemy(){
        Enemy = new GameObject[] { spawnObject, spawnObject2 };
    }


    IEnumerator SpawnCoroutine()
    {
        List<int> countList = new List<int>();
        while (true)
        {

            countList.Clear();
            for(int i = 0; i< targetPositions.Length; i++)
            {
                if (cullingGroup.IsVisible(i) == false)
                    countList.Add(i);
            }

            if(countList.Count != 0)
            {
                var newPos = countList[Random.Range(0, countList.Count)];
                GameObject.Instantiate(Enemy[Random.Range(0,2)], targetPositions [newPos].position, Quaternion.identity);
            }

            yield return new WaitForSeconds(0.5f); 

        }

    }

}
