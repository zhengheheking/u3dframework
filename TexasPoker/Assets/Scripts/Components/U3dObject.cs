namespace Components
{
    using UnityEngine;
    using System.Collections.Generic;

    public class U3dObject
    {
        public static readonly int Layer_Default = 0;
        public static readonly int Layer_UI_2D = 8;
        public static readonly int Layer_Model = 9;

        public GameObject mGameObject { get; set; }
        private Vector3 m_position;			// 绝对坐标
        protected Vector3 m_rotation;		// 绝对旋转
        private Vector3 m_scale;			// 相对缩放
        private Transform m_parent;			// 父节点
        private int m_layer;				// 层
        private bool m_bVisible;
        private string m_Name;
        private List<GameObject> m_children; // 子对象
        public List<GameObject> Children
        {
            get
            {
                return m_children;
            }
        }
        public virtual bool Visible
        {
            get
            {
                if (null != mGameObject)
                    m_bVisible = mGameObject.activeSelf;
                return m_bVisible;
            }
            set
            {
                m_bVisible = value;
                if (null != mGameObject && mGameObject.activeSelf != m_bVisible)
                {
                    mGameObject.SetActive(m_bVisible);
                }
            }
        }

        public Vector3 Position
        {
            get
            {
                if (null != mGameObject)
                    m_position = mGameObject.transform.position;
                return m_position;
            }
            set
            {
                m_position = value;
                if (null != mGameObject)
                {
                    mGameObject.transform.position = m_position;
                }
            }
        }

        public Vector3 LocalPosition
        {
            get
            {
                if (null == Parent)
                    return Position;
                return Parent.InverseTransformPoint(Position);
            }
            set
            {
                if (null == Parent)
                    Position = value;
                else
                    Position = Parent.TransformPoint(value);
            }
        }

        public Vector3 Direction
        {
            get
            {
                if (null != mGameObject)
                    m_rotation = mGameObject.transform.eulerAngles;
                return m_rotation;
            }
            set
            {
                m_rotation = value;
                if (null != mGameObject)
                {
                    mGameObject.transform.rotation = Quaternion.Euler(m_rotation);
                }
            }
        }

        public Vector3 LocalDirection
        {
            get
            {
                if (null == Parent)
                    return Direction;
                return Direction - Parent.transform.eulerAngles;
            }
            set
            {
                if (null == Parent)
                    Direction = value;
                else
                    Direction = Parent.transform.eulerAngles + value;
            }
        }

        public Transform Parent
        {
            get
            {
                if (null != mGameObject)
                    m_parent = mGameObject.transform.parent;
                return m_parent;
            }
            set
            {
                m_parent = value;
                if (null != mGameObject)
                {
                    mGameObject.transform.parent = m_parent;
                }
            }
        }

        public virtual Vector3 Scale
        {
            get
            {
                if (null != mGameObject)
                    m_scale = mGameObject.transform.localScale;
                return m_scale;
            }
            set
            {
                m_scale = value;
                if (null != mGameObject)
                {
                    mGameObject.transform.localScale = m_scale;
                }
            }
        }

        public int Layer
        {
            get
            {
                if (null != mGameObject)
                    m_layer = mGameObject.layer;
                return m_layer;
            }
            set
            {
                m_layer = value;
                if (null != mGameObject)
                {
                    Utils.SetLayer(mGameObject, m_layer);
                }
            }
        }

        public string Name
        {
            get
            {
                if (null != mGameObject)
                    m_Name = mGameObject.name;
                return m_Name;
            }
            set
            {
                m_Name = value;
                if (null != mGameObject)
                {
                    mGameObject.name = value;
                }
            }
        }

        public U3dObject()
        {
            mGameObject = null;
            m_bVisible = true;
            m_position = Vector3.zero;
            m_rotation = Vector3.zero;
            m_scale = Vector3.one;
            m_parent = null;
            m_Name = string.Empty;
            m_children = new List<GameObject>();
        }

        public void AttachGameObject(GameObject go)
        {
            AttachGameObject(go, Vector3.zero, Vector3.zero);
        }

        public void AttachGameObject(GameObject go, Vector3 localPosition, Vector3 localRotation)
        {
            if (null == go || null == mGameObject) return;
            go.transform.parent = mGameObject.transform;
            go.transform.localPosition = localPosition;
            go.transform.localRotation = Quaternion.Euler(localRotation);
            m_children.Add(go);
        }

        public void AttachXU3dObject(U3dObject u3dObject)
        {
            AttachU3dObject(u3dObject, Vector3.zero, Vector3.zero);
        }

        public void AttachU3dObject(U3dObject u3dObject, Vector3 localPosition, Vector3 localRotaion)
        {
            if (null == u3dObject) return;
            AttachGameObject(u3dObject.mGameObject, localPosition, localRotaion);
        }

        private void restoreInfo()
        {
#pragma warning disable
            bool b = Visible;
            Vector3 vec = Position;
            vec = Direction;
            Transform t = Parent;
            vec = Scale;
            int n = Layer;
#pragma warning restore
        }

        private void detachAll()
        {
            for (int i = 0; i < m_children.Count; )
            {
                GameObject go = m_children[i];
                if (null != go && go.transform.parent == mGameObject.transform)
                {
                    go.transform.parent = null;
                    i++;
                    continue;
                }
                m_children.RemoveAt(i);
            }
        }

        private void reAttachAll()
        {
            for (int i = 0; i < m_children.Count; )
            {
                GameObject go = m_children[i];
                if (null != go)
                {
                    go.transform.parent = mGameObject.transform;
                    i++;
                    continue;
                }
                m_children.RemoveAt(i);
            }
        }

        // 重新设置 position / rotation / parent / layer / children
        private void resetGameObject()
        {
            if (null == mGameObject) return;
            mGameObject.transform.parent = m_parent;
            mGameObject.transform.position = m_position;
            mGameObject.transform.rotation = Quaternion.Euler(m_rotation);
            mGameObject.transform.localScale = m_scale;
            Utils.SetLayer(mGameObject, m_layer);
            for (int i = 0; i < m_children.Count; )
            {
                GameObject go = m_children[i];
                if (null == go)
                {
                    m_children.RemoveAt(i);
                    continue;
                }
                go.transform.parent = mGameObject.transform;
                i++;
            }
            if (!m_bVisible) mGameObject.SetActive(false);
            Object.DontDestroyOnLoad(mGameObject);
        }

        public void Destroy()
        {
            Destroy(false);
        }

        public virtual void Destroy(bool bDestroyAll)
        {
            if (null == mGameObject)
                return;

            if (!bDestroyAll)
            {
                for (int i = 0; i < m_children.Count; i++)
                {
                    GameObject go = m_children[i];
                    if (null != go && go.transform.parent == mGameObject.transform)
                    {
                        go.transform.parent = null;
                    }
                }
            }
            m_children.Clear();
            GameObject.Destroy(mGameObject);
        }
    }


}
