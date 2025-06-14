using System.Collections.Generic;

namespace zintersid.Common.ViewModels
{
    public class MenuViewModel
    {
        public List<MenuSection> Sections { get; set; } = new List<MenuSection>();
        public string CurrentPage { get; set; }
    }

    public class MenuSection
    {
        public string Title { get; set; }
        public string TitleKey { get; set; }
        public string Icon { get; set; }
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();
    }

    public class MenuItem
    {
        public string PageName { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public string Href { get; set; }
        public string BadgeText { get; set; }
        public bool IsLeaf { get; set; }
        public List<SubMenuItem> Children { get; set; } = new List<SubMenuItem>();
    }

    public class SubMenuItem
    {
        public string PageName { get; set; }
        public string DisplayName { get; set; }
        public string Href { get; set; }
        public string BadgeText { get; set; }
    }
}