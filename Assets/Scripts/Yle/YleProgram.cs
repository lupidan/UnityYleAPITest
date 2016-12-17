using System.Collections.Generic;

namespace YleService
{
    using JsonDictionary = Dictionary<string, object>;
    using JsonArray = List<object>;

    public class YleProgram
    {
        public string Identifier  { get; private set; }
        public string Title       { get; private set; }
        public string Description { get; private set; }

        public static List<YleProgram> YleProgramsFromJsonArray(JsonArray jsonArray)
        {
            List<YleProgram> programs = new List<YleProgram>();
            for (int i = 0; i < jsonArray.Count; i++)
            {
                JsonDictionary programJsonDictionary = jsonArray[i] as JsonDictionary;
                if (programJsonDictionary != null)
                {
                    programs.Add(new YleProgram(programJsonDictionary));
                }
            }
            return programs;
        }

        public YleProgram(JsonDictionary jsonDictionary)
        {
            Identifier = jsonDictionary["id"] as string;
            JsonDictionary titleJsonDictionary = jsonDictionary["title"] as JsonDictionary;
            if (titleJsonDictionary != null)
                Title = titleJsonDictionary["fi"] as string;

            JsonDictionary descriptionJsonDictionary = jsonDictionary["description"] as JsonDictionary;
            if (descriptionJsonDictionary != null)
                Description = descriptionJsonDictionary["fi"] as string;

        }
    }

}
