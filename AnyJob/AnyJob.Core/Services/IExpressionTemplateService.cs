using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob{

    public interface IExpressionTemplateService
    {
        bool IsExpression(string text);
        string PickExpression(string text);
    }
}
