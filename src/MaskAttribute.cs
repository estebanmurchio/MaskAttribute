using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MaskAttribute : Attribute, IMetadataAware
    {
        public string Selector { get; private set; }
        public string Mask { get; private set; }

        public MaskAttribute(string selector, string mask)
        {
            Selector = selector;
            Mask = mask;
        }

        private const string ScriptText =
            "<script data-eval='true' type='text/javascript'>" +
                "jQuery(document).ready(function () {{" +
                    "jQuery('{0}').mask('{1}');" +
                "}});" +
            "</script>";

        private const string AutoNumericScriptText =
            "<script data-eval='true' type='text/javascript'>" +
                "jQuery(document).ready(function () {{" +
                    "jQuery('{0}').autoNumeric('init', {1});" +
                "}});" +
            "</script>";

        public const string TemplateHint = "_maskedInput";

        internal HttpContextBase Context
        {
            get { return new HttpContextWrapper(HttpContext.Current); }
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            var list = Context.Items["Scripts"] as IList<string> ?? new List<string>();

            metadata.TemplateHint = TemplateHint;
            metadata.AdditionalValues[TemplateHint] = Selector;

            var s = string.Format(Mask.StartsWith("{")
                ? AutoNumericScriptText
                : ScriptText, Selector, Mask);

            if (!list.Contains(s))
                list.Add(s);

            Context.Items["Scripts"] = list;
        }
    }
}
