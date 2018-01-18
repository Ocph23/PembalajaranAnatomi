using Mobile.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    internal class TagEntryViewExamplePageModel
    {
        public TagEntryViewExamplePageModel()
        {
            RemoveTagCommand = new Command((arg) => RemoveTag(arg));
            ReloadTags();
        }

        public void ReloadTags()
        {
            var tags = new ObservableCollection<TagItem>(){
                new TagItem() { Name = "#TagExample" },
                new TagItem() { Name = "#Xamarin" },
                new TagItem() { Name = "#DanielLuberda" },
                new TagItem() { Name = "#Test" },
                new TagItem() { Name = "#XamarinForms" },
                new TagItem() { Name = "#TagEntryView" },
                new TagItem() { Name = "#TapMe!" },
                new TagItem() { Name = "#itsworking!" },
            };

            Items = tags;
        }

        public void RemoveTag(object tag)
        {
            var tagItem = (TagItem)tag;
            if (tagItem == null)
                return;

            Items.Remove(tagItem);
        }

        public TagItem ValidateAndReturn(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                return null;

            var tagString = tag.StartsWith("#") ? tag : "#" + tag;

            if (Items.Any(v => v.Name.Equals(tagString, StringComparison.OrdinalIgnoreCase)))
                return null;

            return new TagItem()
            {
                Name = tagString.ToLower()
            };
        }

        public ICommand RemoveTagCommand { get; set; }

        public ObservableCollection<TagItem> Items
        {
            get;set;
        }

      
    }
}