using System.Collections.Generic;
using UnityEngine;
namespace Components
{
    public class U3dModel:U3dObject
    {
        public U3dModel(string modelName)
        {
            GameObject go = ResourceManager.Instance.GetModelPrefebByName(modelName);
            mGameObject = GameObject.Instantiate(go);
        }
        public U3dModel()
        {

        }
        public U3dModel(GameObject go)
        {
            mGameObject = go;
        }
        public void ChangeMatColor(Color color)
        {
            if (mGameObject == null)
                return;

            MeshRenderer[] renderList1 = mGameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer smr in renderList1)
            {
                foreach (Material mat in smr.materials)
                {
                    mat.color = color;
                }
            }

        }
        public override void Destroy(bool bDestroyAll)
        {
            if (null == mGameObject)
                return;

            if (!bDestroyAll)
            {
                for (int i = 0; i < Children.Count; i++)
                {
                    GameObject go = Children[i];
                    if (null != go && go.transform.parent == mGameObject.transform)
                    {
                        go.transform.parent = null;
                    }
                }
            }
            Children.Clear();
            SpawnPool.Instance.Despawn(mGameObject);
        }
    }
}