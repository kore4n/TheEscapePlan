using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    /// <summary>
    /// Simple class to hold copies of all items
    /// </summary>
    public class ItemManager : MonoBehaviour
    {
        private static ItemManager instance;

        [SerializeField] private ItemListScriptableObject itemListSO;

        public static ItemManager Instance
        {
            get
            {
                return instance;
            }
        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            //foreach (var item in itemListSO.GetItemsListSO()) 
            //{
            //    Debug.Log(item.name);
            //    Debug.Log(item.id);
            //}
        }

        

        public ItemListScriptableObject GetItemListSO()
        {
            return itemListSO;
        }
    }
}