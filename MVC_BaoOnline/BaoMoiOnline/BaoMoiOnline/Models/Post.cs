using System;
using System.Collections.Generic;

#nullable disable

namespace BaoMoiOnline.Models
{
    public partial class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string ShortContents { get; set; }
        public string Contents { get; set; }
        public string Img { get; set; }
        public string Alias { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Author { get; set; }
        public int? AccountId { get; set; }
        public string Tag { get; set; }
        public int? CatId { get; set; }
        public bool IsHost { get; set; }
        public bool IsNewfeed { get; set; }

        public virtual Account Account { get; set; }
        public virtual Category Cat { get; set; }
    }
}
