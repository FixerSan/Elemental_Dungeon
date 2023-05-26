using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class SerializableDictionary<TKey, TVaule> : Dictionary<TKey, TVaule>, ISerializationCallbackReceiver
{
    public List<TKey> InspectorKeys;
    public List<TVaule> InspectorVaule;

    public SerializableDictionary()
    {
        InspectorKeys = new List<TKey>();
        InspectorVaule = new List<TVaule>();
        SyncInspectorFromDictionary();
    }
    public new void Add(TKey key, TVaule value)
    {
        base.Add(key, value);
        SyncInspectorFromDictionary();
    }



    public new void Remove(TKey key)
    {
        base.Remove(key);
        SyncInspectorFromDictionary();
    }

    public void OnBeforeSirialize()
    {

    }
    public void SyncInspectorFromDictionary()
    {
        InspectorKeys.Clear();
        InspectorVaule.Clear();

        foreach (var pair in this)
        {
            InspectorKeys.Add(pair.Key);
            InspectorVaule.Add(pair.Value);
        }
    }

    public void SyncDictionaryFromInspector()
    {
        foreach (var key in Keys.ToList())
        {
            base.Remove(key);
        }

        for (int i = 0; i < InspectorKeys.Count; i++)
        {
            if (this.ContainsKey(InspectorKeys[i]))
            {
                Debug.LogError("Áßº¹ Å°°¡ ÀÖ½À´Ï´Ù.");
                break;
            }

            base.Add(InspectorKeys[i], InspectorVaule[i]);
        }
    }

    public void OnAfterDeserialize()
    {
        if (InspectorKeys.Count == InspectorVaule.Count)
        {
            SyncDictionaryFromInspector();
        }
    }

    public void OnBeforeSerialize()
    {
        if (InspectorKeys.Count == InspectorVaule.Count)
        {
            SyncDictionaryFromInspector();
        }
    }
}