using UnityEngine;
using System.Collections.Generic;
using REProtocol;
using REStructure;
using REStructure.Scenes;

public class CollectMaterial : MonoBehaviour
{
    public void/* bool*/ Collect(CollectionMethod method/*, out Item material*/)
    {
        //material = null;
        if (GameGlobal.Player.Location is ResourcePoint)
        {
            ResourcePoint resourcePoint = GameGlobal.Player.Location as ResourcePoint;
            if (resourcePoint.collectionList.ContainsKey(method))
            {
                Dictionary<Item, int> materials = resourcePoint.collectionList[method];
                var enumerator = materials.GetEnumerator();
                int randomResult = Random.Range(1, 10000);
                while (enumerator.MoveNext() && randomResult > 0)
                {
                    if(randomResult <= enumerator.Current.Value)
                    {
                        Debug.Log(enumerator.Current.Key.name);
                        //material = enumerator.Current.Key;
                        return;// true;
                    }
                    else
                    {
                        randomResult -= enumerator.Current.Value;
                    }
                }
            }
        }
        //return false;
    }
}
