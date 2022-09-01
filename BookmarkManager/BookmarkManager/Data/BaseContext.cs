using System.Collections.Generic;

namespace BookmarkManager.Data
{
    public class BaseContext
    {
        public LinkedList<Collection> CollectionList { get; set; } = new LinkedList<Collection>();
        public LinkedList<Bookmark> BookmarkList { get; set; } = new LinkedList<Bookmark>();
    }
}
