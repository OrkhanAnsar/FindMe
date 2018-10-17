using Plugin.Multilingual;
using System;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindMeMobileClient.Helpers
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        const string ResourceId = "FindMeMobileClient.Resources.AppResources";

        static readonly Lazy<ResourceManager> resmgr = new Lazy<ResourceManager>(() => 
                            new ResourceManager(ResourceId, typeof(TranslateExtension)
                                                            .GetTypeInfo().Assembly));

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            var ci = CrossMultilingual.Current.CurrentCultureInfo;
            var translation = resmgr.Value.GetString(Text, ci);

            if (translation == null)
            {
                translation = this.Text; // returns the key, which GETS DISPLAYED TO THE USER
            }
            return translation;
        }
    }
}
