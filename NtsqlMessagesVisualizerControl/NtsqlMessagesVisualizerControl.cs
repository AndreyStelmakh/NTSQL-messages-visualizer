using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;

namespace Lmx.UI.Controls
{
    [TemplatePart(Name = NtsqlMessagesVisualizerControl.ElementViewRichEditBox, Type = typeof(RichTextBox))]
    public class NtsqlMessagesVisualizerControl : Control
    {
        static NtsqlMessagesVisualizerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NtsqlMessagesVisualizerControl), new FrameworkPropertyMetadata(typeof(NtsqlMessagesVisualizerControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _viewRichEditBox = GetTemplateChild(ElementViewRichEditBox) as RichTextBox;
        }

        private static FlowDocument ToFlowDocument(XDocument document)
        {
            var doc = new FlowDocument();

            doc.Blocks.Add(ToSection(document.Elements().First()));

            return doc;
        }

        private static Section ToSection(XElement element)
        {
            var section = new Section() { Margin = new Thickness(15, 0, 0, 0) };
            var headerAttribute = element.Attribute("text");

            if (headerAttribute != null)
                section.Blocks.Add(new Paragraph(new Run(headerAttribute.Value)) { Margin = new Thickness(0), Padding = new Thickness(0) });

            foreach(var subElement in element.Elements())
            {
                section.Blocks.Add(ToSection(subElement));
            }

            return section;
        }

        public XDocument Value
        {
            get { return (XDocument)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(XDocument), typeof(NtsqlMessagesVisualizerControl), new PropertyMetadata(new PropertyChangedCallback((d, e) =>
            {
                var ctrl = d as NtsqlMessagesVisualizerControl;

                if(ctrl != null && e.NewValue is XDocument)
                {
                    ctrl._viewRichEditBox.Document = ToFlowDocument((XDocument)e.NewValue);
                }
            })));

        private RichTextBox _viewRichEditBox;

        private const string ElementViewRichEditBox = "PART_View";
    }
}
