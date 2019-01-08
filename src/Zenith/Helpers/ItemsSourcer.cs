using System;
using System.Collections;
using Xamarin.Forms;

namespace Zenith
{
    public static class ItemsSourcer
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.CreateAttached("ItemsSource", typeof(IEnumerable), typeof(ItemsSourcer), default(IEnumerable), propertyChanged: OnItemsSourceChanged);

        public static IEnumerable GetItemsSource(BindableObject bindable)
        {
            return (IEnumerable)bindable.GetValue(ItemsSourceProperty);
        }

        public static void SetItemsSource(BindableObject bindable, IEnumerable value)
        {
            bindable.SetValue(ItemsSourceProperty, value);
        }

        static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Repopulate(bindable);
        }

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.CreateAttached("ItemTemplate", typeof(DataTemplate), typeof(ItemsSourcer), default(DataTemplate), propertyChanged: OnItemTemplateChanged);

        public static DataTemplate GetItemTemplate(BindableObject bindable)
        {
            return (DataTemplate)bindable.GetValue(ItemTemplateProperty);
        }

        public static void SetItemTemplate(BindableObject bindable, DataTemplate value)
        {
            bindable.SetValue(ItemTemplateProperty, value);
        }

        static void OnItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Repopulate(bindable);
        }

        static void Repopulate(BindableObject bindable)
        {
            var layout = bindable as Layout<View>;
            if (layout == null)
                return;
            layout.Children.Clear();

            var itemsSource = layout.GetValue(ItemsSourceProperty) as IEnumerable;
            var template = layout.GetValue(ItemTemplateProperty) as DataTemplate;

            if (itemsSource == null || template == null)
                return;

            foreach (var item in itemsSource)
            {
                var content = template.CreateContent() as View;
                if (content == null)
                    continue;
                content.BindingContext = item;
                layout.Children.Add(content);
            }
        }
    }
}
