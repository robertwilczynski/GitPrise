using System;

namespace Gwit.Web.Models
{
    public class ListItemViewModel : IComparable
    {
        public enum ItemType
        {
            Blob,
            Tree
        }

        public string Name { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
        public ItemType Type { get; set; }

        public string Path { get; set; }

        public DateTimeOffset? AuthorDate { get; set; }
        public DateTimeOffset? CommitDate { get; set; }

        

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj", "obj is null.");
            }

            if (!(obj is ListItemViewModel))
            {
                throw new ArgumentNullException("obj", "obj is not ListItemViewModel.");
            }

            return this.Name.CompareTo((obj as ListItemViewModel).Name);
        }
    }
}
