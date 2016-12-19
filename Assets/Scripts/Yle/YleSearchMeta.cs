using System.Collections.Generic;
using UnityEngine;

namespace YleService
{

    public struct YleSearchMeta {
        public string Query      { get; private set; }
        public long Offset       { get; private set; }
        public long Limit        { get; private set; }
        public long Count        { get; private set; }
        public long ProgramCount { get; private set; }
        public long ClipCount    { get; private set; }

        public YleSearchMeta(Dictionary<string, object> dict) : this()
        {
            Query = dict["q"] as string;
            Count = (long) dict["count"];
            ProgramCount = (long) dict["program"];
            ClipCount = (long) dict["program"];

            string receivedOffset = dict["offset"] as string;
            if (receivedOffset != null)
            {
                long offset = 0;
                long.TryParse(receivedOffset, out offset);
                Offset = offset;
            }

            string receivedLimit = dict["limit"] as string;
            if (receivedLimit != null)
            {
                long limit = 0;
                long.TryParse(receivedLimit, out limit);
                Limit = limit;
            }
        }

    }
}