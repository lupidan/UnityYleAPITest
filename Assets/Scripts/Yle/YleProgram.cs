using UnityEngine;
using System.Collections.Generic;

namespace YleService
{

    public class YleProgram
    {
        public string Identifier  { get; private set; }
        public string Title       { get; private set; }
        public string Description { get; private set; }

        public static List<YleProgram> YleProgramsFromJsonArray(List<object> jsonArray)
        {
            List<YleProgram> programs = new List<YleProgram>();
            for (int i = 0; i < jsonArray.Count; i++)
            {
                Dictionary<string, object> programDict = jsonArray[i] as Dictionary<string, object>;
                if (programDict != null)
                {
                    programs.Add(new YleProgram(programDict));
                }
            }
            return programs;
        }

        public YleProgram(Dictionary<string, object> jsonDictionary)
        {
            Identifier = jsonDictionary["id"] as string;

            Dictionary<string, object> titleDict = jsonDictionary["title"] as Dictionary<string, object>;
            object title = "UNKNOWN TITLE";
            if (titleDict != null &&  titleDict.Count > 0 &&  !titleDict.TryGetValue("fi", out title))
                title = "NO FINNISH TITLE";
            Title = title as string;

            Dictionary<string, object> descriptionDict = jsonDictionary["description"] as Dictionary<string, object>;
            object description = "UNKNOWN DESCRIPTION";
            if (descriptionDict != null && descriptionDict.Count > 0 && !descriptionDict.TryGetValue("fi", out description))
                description = "NO FINNISH DESCRIPTION";
            Description = description as string;

        }
    }

}
