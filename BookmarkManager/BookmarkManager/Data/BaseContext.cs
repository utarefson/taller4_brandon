using System.Collections.Generic;

namespace BookmarkManager.Data
{
    public class BaseContext
    {
        public LinkedList<Collection> CollectionList { get; set; } = new LinkedList<Collection>();
        public List<Bookmark> BookmarkList { get; set; } = new List<Bookmark>();
    }
}
