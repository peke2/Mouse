using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.WSA;

public class Navigation : MonoBehaviour {

	NavMeshData navMeshData;
	NavMeshDataInstance navMeshDataInstance;
	List<NavMeshBuildSource> navMeshBuildSourceList = new List<NavMeshBuildSource>();

	public List<GameObject> targetMeshes = new List<GameObject>();


	public GameObject searchTarget;

	bool isReady = false;
	bool isCreated = false;

	public bool isReadyNavigation()
	{
		return isReady;
	}



	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		/*
		if(isReady == false)
		{
			if(isCreated == false)
			{
				isCreated = createNavMesh();
			}
		}*/
	}


	private IEnumerator createNavMesh()
	{
		if(searchTarget == null)
		{
			yield break;
		}

		var sp = searchTarget.GetComponent<SpatialMappingCollider>();
		if(sp == null)
		{
			yield break;
		}

		while(true)
		{
			yield return new WaitForSeconds(5);

			if(sp.surfaceParent != null)
			{
				int ccount = sp.surfaceParent.transform.GetChildCount();
				targetMeshes.Clear();
				for(int i = 0; i < ccount; i++)
				{
					GameObject gobj;
					gobj = sp.surfaceParent.transform.GetChild(i).gameObject;
					targetMeshes.Add(gobj);
				}

				yield return StartCoroutine(updateNavMesh());
			}

			//ここから先はメッシュの更新不要でOK？
			//sp.freezeUpdates = true;
		}
	}


	private void OnEnable()
	{
		navMeshData = new NavMeshData();
		navMeshDataInstance = NavMesh.AddNavMeshData(navMeshData);

		//	場当たりだけど、メッシュがセットされていなければ開始時にナビメッシュを作成しない
		//	テストと実際の使用時の違いの吸収
		if(targetMeshes.Count > 0)
		{
			StartCoroutine(updateNavMesh());
		}

		StartCoroutine(createNavMesh());
	}

	private void OnDisable()
	{
		navMeshDataInstance.Remove();
		StopAllCoroutines();
	}


	IEnumerator updateNavMesh()
	{
		isReady = false;

		navMeshBuildSourceList.Clear();

		foreach(GameObject gobj in targetMeshes)
		{
			MeshFilter mf = gobj.GetComponent<MeshFilter>();
			NavMeshBuildSource nbs = new NavMeshBuildSource();

			if(mf.sharedMesh == null) yield return null;

			nbs.shape = NavMeshBuildSourceShape.Mesh;

			Mesh mesh = mf.sharedMesh;
			if((mesh == null) || (mesh.subMeshCount == 0)) continue;

			nbs.sourceObject = mf.sharedMesh;
			nbs.transform = mf.transform.localToWorldMatrix;
			nbs.area = 0;

			navMeshBuildSourceList.Add(nbs);
		}

		Bounds bounds = new Bounds(Vector3.zero, new Vector3(50,20,50));
		NavMeshBuildSettings settings = NavMesh.GetSettingsByID(0);
		AsyncOperation op;
		op = NavMeshBuilder.UpdateNavMeshDataAsync(navMeshData, settings, navMeshBuildSourceList, bounds);
		while(true)
		{
			if(op.isDone) break;
			yield return null;
		}

		Debug.Log("ナビメッシュ作成完了");
		isReady = true;
	}
}
