using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace AE_SkillEditor_Plus.Factory
{
    public class AETimelineSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        public List<string> paths;

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var entries = new List<SearchTreeEntry>();
            entries.Add(new SearchTreeGroupEntry(new GUIContent("搜索文件"))); //添加了一个一级菜单
            for (var index = 0; index < paths.Count; index++)
            {
                var path = paths[index];
                entries.Add(new SearchTreeEntry(new GUIContent(path.Split("/").Last().Split(".")[0]))
                    { level = 1, userData = paths[index] });
            }

            return entries;
        }

        public Action<string> Callbakc;

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            // Debug.Log(SearchTreeEntry.userData as string);
            Callbakc?.Invoke(SearchTreeEntry.userData as string);
            return true;
        }
    }
}