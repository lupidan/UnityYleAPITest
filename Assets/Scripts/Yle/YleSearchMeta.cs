using System.Collections.Generic;

namespace YleService
{
    using JsonDictionary = Dictionary<string, object>;

    public struct YleSearchMeta {
        public string Query      { get; private set; }
        public long Offset       { get; private set; }
        public long Limit        { get; private set; }
        public long Count        { get; private set; }
        public long ProgramCount { get; private set; }
        public long ClipCount    { get; private set; }

        public YleSearchMeta(JsonDictionary jsonDictionary) : this()
        {
            Query = jsonDictionary["q"] as string;
            Count = (long) jsonDictionary["count"];
            ProgramCount = (long) jsonDictionary["program"];
            ClipCount = (long) jsonDictionary["program"];

            string receivedOffset = jsonDictionary["offset"] as string;
            if (receivedOffset != null)
            {
                long offset = 0;
                long.TryParse(receivedOffset, out offset);
                Offset = offset;
            }

            string receivedLimit = jsonDictionary["limit"] as string;
            if (receivedLimit != null)
            {
                long limit = 0;
                long.TryParse(receivedLimit, out limit);
                Limit = limit;
            }
        }

    }
}