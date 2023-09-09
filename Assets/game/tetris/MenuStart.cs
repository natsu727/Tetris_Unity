        using UnityEngine;
        
        public class MenuStart : MonoBehaviour
        {
        	public Transform Capa;           //Canvas to enable
        	void Start ()
        	{
        		Capa.gameObject.SetActive(true);
        	}
        }
