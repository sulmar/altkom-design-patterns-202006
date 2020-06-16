using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Text;

namespace VisitorPattern.Solution
{

    public class VisitorTest
    {
        public static void Test()
        {
            Form form = Get();

            IVisitor visitor = new HtmlVisitor();

            form.Accept(visitor);

            string output = visitor.Output;

            Console.WriteLine(output);

            System.IO.File.AppendAllText("index.html", output);

            ExpandoObjectTest();

            DynamicTest();
        }

        private static void DynamicTest()
        {
            // DLR

            dynamic x = 10;            

            x = "Hello World";

            // ExpandoObject

        }

        private static void ExpandoObjectTest()
        {
            dynamic expand = new ExpandoObject();

            expand.Name = "Rick";
            expand.HelloWorld = (Func<string, string>)((string name) =>
            {
                return "Hello " + name;
            });

            Console.WriteLine(expand.Name);
            Console.WriteLine(expand.HelloWorld("Dufus"));
        }

        // Lua 

        public static Form Get()
        {
            Form form = new Form
            {
                Name = "/forms/customers",
                Title = "Design Patterns",

                Body = new Collection<Control>
                {
                    new LabelControl { Caption = "Person", Name = "lblName" },
                    new TextBoxControl { Caption = "FirstName", Name = "txtFirstName", Value = "John"},
                    new CheckboxControl { Caption = "IsAdult", Name = "chkIsAdult", Value = true },
                    new ButtonControl {  Caption = "Submit", Name = "btnSubmit", ImageSource = "save.png" },
                }
            };

            return form;
        }
    }

    #region Models

    public class Form
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public ICollection<Control> Body { get; set; }

        public void Accept(IVisitor visitor)
        {            
            foreach (var control in Body)
            {
                control.Accept(visitor);
            }
        }
    }

    // Abstract Element
    public abstract class Control
    {
        public string Name { get; set; }
        public string Caption { get; set; }

        public abstract void Accept(IVisitor visitor);
    }

    public abstract class Control<T> : Control
    {
        public T Value { get; set; }
    }

    // Concrete Element
    public class LabelControl : Control
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        // BAD PRACTICE!!!
        //public override string ToString()
        //{            
        //    return $"<span>{Caption}</title>";
        //}
    }

    // Concrete Element
    public class TextBoxControl : Control<string>
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    // Concrete Element
    public class CheckboxControl : Control<bool>
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    // Concrete Element
    public class ButtonControl : Control
    {
        public string ImageSource { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    #endregion

    // Abstract Visitor
    public interface IVisitor
    {
        void Visit(LabelControl control);
        void Visit(TextBoxControl control);
        void Visit(CheckboxControl control);
        void Visit(ButtonControl control);

        string Output { get; }
    }

    // Concrete Visitor A
    public class HtmlVisitor : IVisitor
    {
        private StringBuilder builder = new StringBuilder();

        public string Output
        {
            get
            {
                AppendEndDocument();
                return builder.ToString();
            }
        }

        private void AppendEndDocument()
        {
            builder.AppendLine("</html>");
        }

        public HtmlVisitor()
        {
            builder.AppendLine("<html>");
        }

        public void Visit(LabelControl control)
        {
            builder.AppendLine($"<span>{control.Caption}</span>");
        }

        public void Visit(TextBoxControl control)
        {
            builder.AppendLine($"<span>{control.Caption}<input type='text' value='{control.Value}'></input></span>");
        }

        public void Visit(CheckboxControl control)
        {
            builder.AppendLine($"<span>{control.Caption}<input type='checkbox' value='{control.Value}'></input></span>");
        }

        public void Visit(ButtonControl control)
        {
            builder.AppendLine($"<button><img src='{control.ImageSource}'/>{control.Caption}</button>");
        }

    }

    // Concrete Visitor B
    public class MarkdownVisitor : IVisitor
    {
        public string Output => throw new NotImplementedException();

        public void Visit(LabelControl control)
        {
            throw new NotImplementedException();
        }

        public void Visit(TextBoxControl control)
        {
            throw new NotImplementedException();
        }

        public void Visit(CheckboxControl control)
        {
            throw new NotImplementedException();
        }

        public void Visit(ButtonControl control)
        {
            throw new NotImplementedException();
        }
    }

    public class PdfVisitor : IVisitor
    {
        public string Output => throw new NotImplementedException();

        public void Visit(LabelControl control)
        {
            throw new NotImplementedException();
        }

        public void Visit(TextBoxControl control)
        {
            throw new NotImplementedException();
        }

        public void Visit(CheckboxControl control)
        {
            throw new NotImplementedException();
        }

        public void Visit(ButtonControl control)
        {
            throw new NotImplementedException();
        }
    }




}
